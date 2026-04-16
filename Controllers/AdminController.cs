using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RestauranteContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment; // NOVO: Permite salvar o arquivo fisicamente na pasta wwwroot

        // Injeção do IWebHostEnvironment adicionada ao construtor
        public AdminController(RestauranteContext context, UserManager<Usuario> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        // ==========================================
        // 1. GERENCIAMENTO DOS PRATOS (ITENS DO CARDÁPIO)
        // ==========================================

        [HttpGet]
        public IActionResult ObterItensCardapio(string filtro = "ativos")
        {
            var query = _context.ItensCardapio
                .Include(i => i.Ingredientes)
                .AsQueryable();

            if (filtro == "ativos")
                query = query.Where(i => i.IsAtivo);
            else if (filtro == "inativos")
                query = query.Where(i => !i.IsAtivo);

            var itens = query
                .Select(i => new ItemCardapioViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    PrecoBase = i.PrecoBase,
                    Periodo = (int)i.Periodo,
                    IsAtivo = i.IsAtivo,
                    ImagemUrl = i.ImagemUrl, // Adicionado para carregar a imagem na tela
                    IngredientesIds = i.Ingredientes
                                        .Where(ing => ing.IsAtivo)
                                        .Select(ing => ing.Id)
                                        .ToList()
                }).ToList();

            return PartialView("_GerenciarCardapio", itens);
        }

        // NOVO: Mudamos para [FromForm] para suportar envio de arquivos, e adicionamos o IFormFile
        [HttpPost]
        public async Task<IActionResult> SalvarItemCardapio([FromForm] ItemCardapioViewModel model, IFormFile imagemArquivo)
        {
            // Removemos a validação padrão de texto da URL porque agora tratamos o arquivo físico
            ModelState.Remove("ImagemUrl");

            // CORREÇÃO: Avisamos ao C# para não dar erro se o usuário não enviar nenhum arquivo físico
            ModelState.Remove("imagemArquivo");

            if (!ModelState.IsValid)
            {
                var erros = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest($"Atenção, dados inválidos:\n{erros}");
            }

            try
            {
                // 1. Lógica de salvamento do arquivo físico no seu computador/servidor
                if (imagemArquivo != null && imagemArquivo.Length > 0)
                {
                    // Define onde salvar: wwwroot/images/cardapio
                    string pastaUploads = Path.Combine(_webHostEnvironment.WebRootPath, "images", "cardapio");

                    if (!Directory.Exists(pastaUploads))
                        Directory.CreateDirectory(pastaUploads);

                    // Cria um nome único para evitar sobrescrever imagens com o mesmo nome
                    string nomeUnicoArquivo = Guid.NewGuid().ToString() + "_" + Path.GetFileName(imagemArquivo.FileName);
                    string caminhoArquivo = Path.Combine(pastaUploads, nomeUnicoArquivo);

                    // Copia o arquivo do navegador para a pasta
                    using (var fileStream = new FileStream(caminhoArquivo, FileMode.Create))
                    {
                        await imagemArquivo.CopyToAsync(fileStream);
                    }

                    // Guarda apenas a rota para o banco de dados
                    model.ImagemUrl = "/images/cardapio/" + nomeUnicoArquivo;
                }

                // 2. Lógica de salvar os dados no banco
                var ids = model.IngredientesIds ?? new List<int>();
                var ingredientesSelecionados = await _context.Ingredientes
                    .Where(i => ids.Contains(i.Id) && i.IsAtivo)
                    .ToListAsync();

                if (model.Id.HasValue && model.Id.Value > 0) // EDIÇÃO
                {
                    var itemDb = await _context.ItensCardapio
                        .Include(i => i.Ingredientes)
                        .FirstOrDefaultAsync(i => i.Id == model.Id);

                    if (itemDb != null)
                    {
                        itemDb.Nome = model.Nome;
                        itemDb.Descricao = model.Descricao;
                        itemDb.PrecoBase = model.PrecoBase;
                        itemDb.Periodo = (ItemCardapio.PeriodoCardapio)model.Periodo;

                        // Só atualiza a imagem no banco se o usuário tiver escolhido um arquivo novo
                        if (!string.IsNullOrEmpty(model.ImagemUrl))
                        {
                            itemDb.ImagemUrl = model.ImagemUrl;
                        }

                        itemDb.Ingredientes.Clear();
                        foreach (var ing in ingredientesSelecionados)
                        {
                            itemDb.Ingredientes.Add(ing);
                        }

                        _context.ItensCardapio.Update(itemDb);
                    }
                }
                else // CRIAÇÃO
                {
                    // Se o usuário não enviou foto, o sistema preenche com a foto padrão automaticamente
                    if (string.IsNullOrEmpty(model.ImagemUrl))
                    {
                        model.ImagemUrl = "/sem-foto.png";
                    }

                    var novoItem = new ItemCardapio
                    {
                        Nome = model.Nome,
                        Descricao = model.Descricao,
                        PrecoBase = model.PrecoBase,
                        Periodo = (ItemCardapio.PeriodoCardapio)model.Periodo,
                        ImagemUrl = model.ImagemUrl, // Vai salvar a foto enviada ou a /sem-foto.png
                        IsAtivo = true,
                        Ingredientes = ingredientesSelecionados
                    };
                    _context.ItensCardapio.Add(novoItem);
                }

                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Prato salvo com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro interno ao salvar: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AlternarStatusPrato(int id)
        {
            var prato = await _context.ItensCardapio.FindAsync(id);
            if (prato == null) return NotFound("Prato não encontrado.");

            prato.IsAtivo = !prato.IsAtivo;

            _context.ItensCardapio.Update(prato);
            await _context.SaveChangesAsync();

            string status = prato.IsAtivo ? "ativado" : "desativado";
            return Ok(new { mensagem = $"Prato {status} com sucesso!" });
        }

        // ==========================================
        // 2. GERENCIAMENTO DOS INGREDIENTES
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterIngredientes()
        {
            // Alterado para trazer todos (ativos e inativos) ordenados
            var ingredientes = await _context.Ingredientes
                .Select(i => new { i.Id, i.Nome, i.IsAtivo })
                .OrderByDescending(i => i.IsAtivo)
                .ThenBy(i => i.Nome)
                .ToListAsync();
            return Json(ingredientes);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarIngredienteRapido([FromBody] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("O nome não pode ser vazio.");

            var novo = new Ingrediente { Nome = nome, IsAtivo = true };
            _context.Ingredientes.Add(novo);
            await _context.SaveChangesAsync();

            return Json(new { id = novo.Id, nome = novo.Nome });
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarIngrediente(int id, [FromBody] Ingrediente model)
        {
            if (string.IsNullOrWhiteSpace(model.Nome)) return BadRequest("Nome inválido.");

            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente == null) return NotFound();

            ingrediente.Nome = model.Nome;
            _context.Ingredientes.Update(ingrediente);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AlternarStatusIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente != null)
            {
                ingrediente.IsAtivo = !ingrediente.IsAtivo;
                _context.Ingredientes.Update(ingrediente);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        // ==========================================
        // 3. GERENCIAMENTO DE USUÁRIOS
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterUsuarios(bool mostrarInativos = false)
        {
            var admins = await _userManager.GetUsersInRoleAsync("Admin");
            var adminIds = admins.Select(a => a.Id).ToList();

            var query = _context.Usuarios.Where(u => !adminIds.Contains(u.Id));

            if (!mostrarInativos)
            {
                query = query.Where(u => u.IsAtivo);
            }

            var usuarios = await query.Select(u => new UsuarioListaViewModel
            {
                Id = u.Id,
                NomeCompleto = u.Nome + " " + u.Sobrenome,
                NomeUsuario = u.UserName,
                CPF = u.CPF,
                IsAtivo = u.IsAtivo
            }).ToListAsync();

            ViewBag.MostrarInativos = mostrarInativos;
            return PartialView("_GerenciarUsuarios", usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> AlternarStatusUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null) return NotFound("Usuário não encontrado.");

            var adminAtual = await _userManager.GetUserAsync(User);
            if (adminAtual != null && adminAtual.Id == id)
            {
                return BadRequest("Você não pode desativar a sua própria conta.");
            }

            usuario.IsAtivo = !usuario.IsAtivo;
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // ==========================================
        // 4. GERENCIAMENTO DE SUGESTÕES DO CHEFE
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterSugestoes()
        {
            var hoje = DateTime.Today;
            var sugestoesHoje = await _context.SugestoesChefe
                .Include(s => s.ItemCardapio)
                .Where(s => s.DataSugestao.Date == hoje)
                .ToListAsync();

            var pratos = await _context.ItensCardapio
                .Where(i => i.IsAtivo)
                .ToListAsync();

            ViewBag.SugestaoAlmoco = sugestoesHoje.FirstOrDefault(s => s.ItemCardapio.Periodo == ItemCardapio.PeriodoCardapio.Almoco);
            ViewBag.SugestaoJantar = sugestoesHoje.FirstOrDefault(s => s.ItemCardapio.Periodo == ItemCardapio.PeriodoCardapio.Jantar);

            return PartialView("_GerenciarSugestoes", pratos);
        }

        [HttpPost]
        public async Task<IActionResult> DefinirSugestao(int pratoId)
        {
            var prato = await _context.ItensCardapio.FindAsync(pratoId);
            if (prato == null) return NotFound();

            var hoje = DateTime.Today;
            var antigas = await _context.SugestoesChefe
                .Include(s => s.ItemCardapio)
                .Where(s => s.DataSugestao.Date == hoje && s.ItemCardapio.Periodo == prato.Periodo)
                .ToListAsync();

            _context.SugestoesChefe.RemoveRange(antigas);

            var novaSugestao = new SugestaoChefe
            {
                ItemCardapioId = pratoId,
                DataSugestao = hoje,
                DescontoAplicado = 20.00m
            };

            _context.SugestoesChefe.Add(novaSugestao);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> SortearSugestao(int periodo)
        {
            var pEnum = (ItemCardapio.PeriodoCardapio)periodo;
            var pratoSorteado = await _context.ItensCardapio
                .Where(i => i.IsAtivo && i.Periodo == pEnum)
                .OrderBy(r => Guid.NewGuid())
                .FirstOrDefaultAsync();

            if (pratoSorteado == null) return BadRequest("Nenhum prato cadastrado para este período.");

            return await DefinirSugestao(pratoSorteado.Id);
        }

        // ==========================================
        // 5. RELATÓRIOS
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterRelatorios(DateTime? dataInicio, DateTime? dataFim)
        {
            DateTime inicio = dataInicio ?? DateTime.Today.AddDays(-30);
            DateTime fim = dataFim ?? DateTime.Today;
            DateTime fimAjustado = fim.Date.AddDays(1).AddTicks(-1);

            var pedidosPeriodo = await _context.Pedidos
                .Include(p => p.Atendimento)
                .Include(p => p.ItensPedido)
                    .ThenInclude(i => i.ItemCardapio)
                .Where(p => p.DataHora >= inicio && p.DataHora <= fimAjustado && !p.IsCancelado)
                .ToListAsync();

            var relatorio = new RelatorioViewModel
            {
                DataInicio = inicio,
                DataFim = fim
            };

            relatorio.FaturamentoPresencial = pedidosPeriodo
                .Where(p => p.Atendimento is AtendimentoRetirada)
                .Sum(p => p.PrecoFinal);

            relatorio.FaturamentoDeliveryProprio = pedidosPeriodo
                .Where(p => p.Atendimento is AtendimentoDeliveryProprio)
                .Sum(p => p.PrecoFinal);

            relatorio.FaturamentoDeliveryApp = pedidosPeriodo
                .Where(p => p.Atendimento is AtendimentoDeliveryApp)
                .Sum(p => p.PrecoFinal);

            relatorio.ItensMaisVendidos = pedidosPeriodo
                .SelectMany(p => p.ItensPedido)
                .GroupBy(i => i.ItemCardapio.Nome)
                .Select(g => new ItemVendidoViewModel
                {
                    NomePrato = g.Key,
                    QuantidadeTotal = g.Sum(i => i.Quantidade),
                    QuantidadeComoSugestao = g.Where(i => i.PrecoUnitario < i.ItemCardapio.PrecoBase).Sum(i => i.Quantidade)
                })
                .OrderByDescending(i => i.QuantidadeTotal)
                .Take(15)
                .ToList();

            return PartialView("_Relatorios", relatorio);
        }
    }
}