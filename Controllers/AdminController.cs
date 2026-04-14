using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;

namespace restaurante.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly RestauranteContext _context;
        private readonly UserManager<Usuario> _userManager;

        public AdminController(RestauranteContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // ==========================================
        // 1. GERENCIAMENTO DOS PRATOS (ITENS DO CARDÁPIO)
        // ==========================================

        [HttpGet]
        public IActionResult ObterItensCardapio()
        {
            var itens = _context.ItensCardapio
                .Include(i => i.Ingredientes)
                .Where(i => i.IsAtivo)
                .Select(i => new ItemCardapioViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    PrecoBase = i.PrecoBase,
                    Periodo = (int)i.Periodo,
                    IngredientesIds = i.Ingredientes
                                        .Where(ing => ing.IsAtivo)
                                        .Select(ing => ing.Id)
                                        .ToList()
                }).ToList();

            return PartialView("_GerenciarCardapio", itens);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarItemCardapio([FromBody] ItemCardapioViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var erros = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest($"Atenção, dados inválidos:\n{erros}");
            }

            try
            {
                var ids = model.IngredientesIds ?? new List<int>();

                var ingredientesSelecionados = await _context.Ingredientes
                    .Where(i => ids.Contains(i.Id) && i.IsAtivo)
                    .ToListAsync();

                if (model.Id.HasValue && model.Id.Value > 0)
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
                        itemDb.IsAtivo = true;

                        itemDb.Ingredientes.Clear();
                        foreach (var ing in ingredientesSelecionados)
                        {
                            itemDb.Ingredientes.Add(ing);
                        }

                        _context.ItensCardapio.Update(itemDb);
                    }
                }
                else
                {
                    var novoItem = new ItemCardapio
                    {
                        Nome = model.Nome,
                        Descricao = model.Descricao,
                        PrecoBase = model.PrecoBase,
                        Periodo = (ItemCardapio.PeriodoCardapio)model.Periodo,
                        IsAtivo = true,
                        Ingredientes = ingredientesSelecionados
                    };
                    _context.ItensCardapio.Add(novoItem);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro interno ao salvar no banco: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoverItemCardapio(int id)
        {
            var item = await _context.ItensCardapio.FindAsync(id);
            if (item != null)
            {
                item.IsAtivo = false;
                _context.ItensCardapio.Update(item);

                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }


        // ==========================================
        // 2. GERENCIAMENTO DOS INGREDIENTES
        // ==========================================

        [HttpGet]
        public async Task<IActionResult> ObterIngredientes()
        {
            var ingredientes = await _context.Ingredientes
                .Where(i => i.IsAtivo)
                .Select(i => new { i.Id, i.Nome })
                .ToListAsync();
            return Json(ingredientes);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarIngredienteRapido([FromBody] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("O nome não pode ser vazio.");

            var novo = new Ingrediente
            {
                Nome = nome,
                IsAtivo = true
            };

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
        public async Task<IActionResult> RemoverIngrediente(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            if (ingrediente != null)
            {
                ingrediente.IsAtivo = false;
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
            // Oculta os Administradores da lista para evitar acidentes
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

            // Busca o que já está definido para hoje
            var sugestoesHoje = await _context.SugestoesChefe
                .Include(s => s.ItemCardapio)
                .Where(s => s.DataSugestao.Date == hoje)
                .ToListAsync();

            // Busca todos os pratos ativos para o Admin escolher
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

            // 1. Remove qualquer sugestão existente para o MESMO PERÍODO no dia de hoje
            var antigas = await _context.SugestoesChefe
                .Include(s => s.ItemCardapio)
                .Where(s => s.DataSugestao.Date == hoje && s.ItemCardapio.Periodo == prato.Periodo)
                .ToListAsync();

            _context.SugestoesChefe.RemoveRange(antigas);

            // 2. Adiciona a nova sugestão com os 20% de desconto
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
        public async Task<IActionResult> SortearSugestao(int periodo) // 0 = Almoco, 1 = Jantar
        {
            var pEnum = (ItemCardapio.PeriodoCardapio)periodo;

            // Busca um prato aleatório que seja ativo e do período correto
            var pratoSorteado = await _context.ItensCardapio
                .Where(i => i.IsAtivo && i.Periodo == pEnum)
                .OrderBy(r => Guid.NewGuid()) // Truque do SQL para sortear
                .FirstOrDefaultAsync();

            if (pratoSorteado == null) return BadRequest("Nenhum prato cadastrado para este período.");

            return await DefinirSugestao(pratoSorteado.Id);
        }
    }
}