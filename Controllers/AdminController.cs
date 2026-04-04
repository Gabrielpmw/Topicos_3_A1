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

        [HttpGet]
        public IActionResult ObterItensCardapio()
        {
            var itens = _context.ItensCardapio.Include(i => i.Ingredientes)
                .Select(i => new ItemCardapioViewModel
                {
                    Id = i.Id,
                    Nome = i.Nome,
                    Descricao = i.Descricao,
                    PrecoBase = i.PrecoBase,
                    Periodo = (int)i.Periodo, // Converte Enum para int
                    IngredientesIds = i.Ingredientes.Select(ing => ing.Id).ToList()
                }).ToList();

            return PartialView("_GerenciarCardapio", itens);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarItemCardapio([FromBody] ItemCardapioViewModel model)
        {
            // 1. BLINDAGEM: Se houver erro, devolvemos a mensagem EXATA para o JavaScript
            if (!ModelState.IsValid)
            {
                var erros = string.Join("\n", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return BadRequest($"Atenção, dados inválidos:\n{erros}");
            }

            try
            {
                // Protege caso a lista venha nula do frontend
                var ids = model.IngredientesIds ?? new List<int>();

                var ingredientesSelecionados = await _context.Ingredientes
                    .Where(i => ids.Contains(i.Id))
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
                        itemDb.Periodo = (ItemCardapio.PeriodoCardapio)model.Periodo; // Converte int para Enum

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
                        Periodo = (ItemCardapio.PeriodoCardapio)model.Periodo, // Converte int para Enum
                        Ingredientes = ingredientesSelecionados
                    };
                    _context.ItensCardapio.Add(novoItem);
                }

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                // Captura erros ocultos de banco de dados
                return BadRequest($"Erro interno ao salvar no banco: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> RemoverItemCardapio(int id)
        {
            var item = await _context.ItensCardapio.FindAsync(id);
            if (item != null)
            {
                _context.ItensCardapio.Remove(item);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> ObterIngredientes()
        {
            var ingredientes = await _context.Ingredientes
                .Select(i => new { i.Id, i.Nome })
                .ToListAsync();
            return Json(ingredientes);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarIngredienteRapido([FromBody] string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return BadRequest("O nome não pode ser vazio.");

            var novo = new Ingrediente { Nome = nome };
            _context.Ingredientes.Add(novo);
            await _context.SaveChangesAsync();

            return Json(new { id = novo.Id, nome = novo.Nome });
        }
    }
}