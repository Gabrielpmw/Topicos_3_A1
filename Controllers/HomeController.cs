using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestauranteContext _context;

        // Injetando o contexto do banco de dados no construtor
        public HomeController(RestauranteContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var hoje = DateTime.Today;

            // Pegamos a hora exata do servidor neste momento
            var horaAtual = DateTime.Now.TimeOfDay;

            // 1. Regras de negócio: Horários permitidos para pedidos
            // Almoço: 11h às 15h | Jantar: 18h às 23h
            bool podeAlmoco = horaAtual >= new TimeSpan(8, 0, 0) && horaAtual <= new TimeSpan(15, 0, 0);
            bool podeJantar = horaAtual >= new TimeSpan(18, 0, 0) && horaAtual <= new TimeSpan(23, 0, 0);

            // 2. Busca no banco quais são os IDs das Sugestões do Chefe sorteadas para hoje
            var sugestoesHoje = await _context.SugestoesChefe
                .Where(s => s.DataSugestao.Date == hoje)
                .Select(s => s.ItemCardapioId)
                .ToListAsync();

            // 3. Busca todos os pratos que estão ativos, trazendo a lista de ingredientes junto
            var pratosDb = await _context.ItensCardapio
                .Include(i => i.Ingredientes)
                .Where(i => i.IsAtivo)
                .ToListAsync();

            // 4. Monta a lista de exibição convertendo os dados do banco para a nossa ViewModel
            var pratos = pratosDb.Select(p => new PratoExibicaoViewModel
            {
                Id = p.Id,
                Nome = p.Nome,
                Descricao = p.Descricao,
                PrecoBase = p.PrecoBase,
                Periodo = (int)p.Periodo,

                // Trazendo a imagem
                ImagemUrl = p.ImagemUrl,

                // Junta os ingredientes ativos separados por vírgula
                Ingredientes = string.Join(", ", p.Ingredientes.Where(ing => ing.IsAtivo).Select(ing => ing.Nome)),

                IsSugestao = sugestoesHoje.Contains(p.Id),

                // Regra do Desconto de 20%
                PrecoFinal = sugestoesHoje.Contains(p.Id) ? p.PrecoBase * 0.80m : p.PrecoBase
            }).ToList();

            // 5. Separa as listas para enviar para a tela
            var model = new HomeViewModel
            {
                Sugestoes = pratos.Where(p => p.IsSugestao).ToList(),
                ItensAlmoco = pratos.Where(p => p.Periodo == 0).ToList(),
                ItensJantar = pratos.Where(p => p.Periodo == 1).ToList(),

                // Variáveis cruciais para bloquear os botões na tela
                PodePedirAlmoco = podeAlmoco,
                PodePedirJantar = podeJantar
            };

            return View(model);
        }
    }
}