using Microsoft.AspNetCore.Authorization;
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

        public AdminController(RestauranteContext context)
        {
            _context = context;
        }

        // ==========================================
        // 1. GERENCIAMENTO DOS PRATOS (ITENS DO CARDÁPIO)
        // ==========================================

        [HttpGet]
        public IActionResult ObterItensCardapio()
        {
            // Busca apenas os pratos que estão ATIVOS e seus ingredientes ATIVOS
            var itens = _context.ItensCardapio
                .Include(i => i.Ingredientes)
                .Where(i => i.IsAtivo) // <-- SOFT DELETE: Filtro aplicado
                .Select(i => new ItemCardapioViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    PrecoBase = i.PrecoBase,
                    Periodo = (int)i.Periodo,
                    IngredientesIds = i.Ingredientes
                                        .Where(ing => ing.IsAtivo) // <-- Filtra ingredientes inativos
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

                // Puxa apenas ingredientes que também estão ativos
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
                        // Garante que o prato volte a ficar ativo caso estivesse desativado e foi reeditado
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
                        IsAtivo = true, // Nasce ativo por padrão
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
                // SOFT DELETE: Ao invés de usar o _context.Remove(item), apenas "desligamos" ele
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
            // Busca apenas os ingredientes que estão ATIVOS
            var ingredientes = await _context.Ingredientes
                .Where(i => i.IsAtivo) // <-- SOFT DELETE: Filtro aplicado
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
                IsAtivo = true // Nasce ativo por padrão
            };

            _context.Ingredientes.Add(novo);
            await _context.SaveChangesAsync();

            return Json(new { id = novo.Id, nome = novo.Nome });
        }

        // --- MÉTODOS NOVOS PARA O LÁPIS E O X FUNCIONAREM ---

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
                ingrediente.IsAtivo = false; // Soft Delete do Ingrediente
                _context.Ingredientes.Update(ingrediente);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }
    }
}