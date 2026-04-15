using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante.Controllers
{
    [Authorize]
    public class PedidoController : Controller
    {
        private readonly RestauranteContext _context;
        private readonly UserManager<Usuario> _userManager;

        public PedidoController(RestauranteContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> CriarPedido([FromBody] FinalizarPedidoViewModel model)
        {
            if (User.IsInRole("Admin"))
                return BadRequest("Admins não podem realizar pedidos.");

            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            if (model.Itens == null || !model.Itens.Any())
                return BadRequest("O carrinho está vazio.");

            var hoje = DateTime.Today;
            decimal subtotal = 0;
            var itensPedido = new List<ItemPedido>();

            // 1. Processa cada item do carrinho
            foreach (var itemCarrinho in model.Itens)
            {
                var prato = await _context.ItensCardapio.FindAsync(itemCarrinho.Id);
                if (prato == null) continue;

                bool isSugestao = await _context.SugestoesChefe
                    .AnyAsync(s => s.ItemCardapioId == prato.Id && s.DataSugestao.Date == hoje);

                decimal precoUnitario = isSugestao ? prato.PrecoBase * 0.8m : prato.PrecoBase;
                subtotal += precoUnitario * itemCarrinho.Quantidade;

                itensPedido.Add(new ItemPedido
                {
                    ItemCardapioId = prato.Id,
                    Quantidade = itemCarrinho.Quantidade,
                    PrecoUnitario = precoUnitario
                });
            }

            if (!itensPedido.Any()) return BadRequest("Nenhum item válido no carrinho.");

            // 2. Calcula Taxas de Atendimento
            Atendimento atendimento;
            decimal taxa = 0;

            if (model.TipoAtendimento == 1) // Delivery Próprio
            {
                taxa = 8.00m;
                atendimento = new AtendimentoDeliveryProprio
                {
                    EnderecoEntregaId = model.EnderecoId ?? 0,
                    Status = StatusDelivery.EmProcesso,
                    TaxaEntrega = taxa
                };
            }
            else if (model.TipoAtendimento == 2) // App
            {
                var horaAtual = DateTime.Now.Hour;
                decimal porcentagemApp = (horaAtual >= 18 || horaAtual < 5) ? 0.06m : 0.04m;
                taxa = subtotal * porcentagemApp;

                atendimento = new AtendimentoDeliveryApp
                {
                    NomeAplicativo = model.NomeApp ?? "App Parceiro",
                    EnderecoEntregaId = model.EnderecoId ?? 0,
                    Status = StatusDelivery.EmProcesso,
                    TaxaEntrega = taxa
                };
            }
            else // Retirada (0)
            {
                atendimento = new AtendimentoRetirada
                {
                    Status = AtendimentoRetirada.StatusRetirada.EmProcesso,
                    TaxaEntrega = 0
                };
            }

            // 3. Salva o Pedido
            var novoPedido = new Pedido
            {
                UsuarioId = usuario.Id,
                DataHora = DateTime.Now,
                PrecoFinal = subtotal + taxa,
                IsCancelado = false,
                Atendimento = atendimento,
                ItensPedido = itensPedido
            };

            _context.Pedidos.Add(novoPedido);

            // 4. Configura o Pagamento
            var pagamento = new Pagamento
            {
                Pedido = novoPedido,
                Metodo = (MetodoPagamento)model.MetodoPagamento,
                IsPago = false
            };

            if (pagamento.Metodo == MetodoPagamento.Pix)
            {
                string idPix = Guid.NewGuid().ToString("N").Substring(0, 10);
                pagamento.ChavePixCopiaECola = $"00020126580014br.gov.bcb.pix0136{idPix}5204000053039865802BR5914Palmas6009SaborArte62070503***63044A1B";
            }

            _context.Pagamentos.Add(pagamento);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Pedido gerado com sucesso!",
                pedidoId = novoPedido.Id,
                chavePix = pagamento.ChavePixCopiaECola
            });
        }

        [HttpGet]
        public async Task<IActionResult> ObterMeusPedidos()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            var pedidos = await _context.Pedidos
                .Include(p => p.Atendimento)
                .Include(p => p.ItensPedido)
                    .ThenInclude(i => i.ItemCardapio)
                .Where(p => p.UsuarioId == usuario.Id)
                .OrderByDescending(p => p.DataHora)
                .ToListAsync();

            return PartialView("_MeusPedidos", pedidos);
        }

        [HttpPost]
        public async Task<IActionResult> CancelarPedido(int id)
        {
            var pedido = await _context.Pedidos
                .Include(p => p.Atendimento)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pedido == null) return NotFound("Pedido não encontrado.");

            bool podeCancelar = false;

            if (pedido.Atendimento is AtendimentoRetirada retirada)
            {
                if (retirada.Status == AtendimentoRetirada.StatusRetirada.EmProcesso)
                {
                    retirada.Status = AtendimentoRetirada.StatusRetirada.Cancelado;
                    podeCancelar = true;
                }
            }
            else if (pedido.Atendimento is AtendimentoDeliveryProprio proprio)
            {
                if (proprio.Status == StatusDelivery.EmProcesso)
                {
                    proprio.Status = StatusDelivery.Cancelado;
                    podeCancelar = true;
                }
            }
            else if (pedido.Atendimento is AtendimentoDeliveryApp app)
            {
                if (app.Status == StatusDelivery.EmProcesso)
                {
                    app.Status = StatusDelivery.Cancelado;
                    podeCancelar = true;
                }
            }

            if (!podeCancelar)
                return BadRequest("Este pedido já avançou e não pode mais ser cancelado.");

            pedido.IsCancelado = true;
            await _context.SaveChangesAsync();

            return Ok(new { mensagem = "Pedido cancelado com sucesso." });
        }

        [HttpGet]
        public async Task<IActionResult> ObterMeusEnderecos()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            var enderecos = await _context.Enderecos
                .Where(e => e.UsuarioId == usuario.Id && e.IsAtivo)
                .ToListAsync();

            return Json(enderecos);
        }
    }
}