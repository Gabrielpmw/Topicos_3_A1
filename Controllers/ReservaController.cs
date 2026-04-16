using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using restaurante.Data;
using restaurante.Models;
using restaurante.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace restaurante.Controllers
{
    [Authorize]
    public class ReservaController : Controller
    {
        private readonly RestauranteContext _context;
        private readonly UserManager<Usuario> _userManager;

        public ReservaController(RestauranteContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> ObterMesasDisponiveis(DateTime dataHora)
        {
            DateTime fimPretendido = dataHora.AddHours(3);
            DateTime limiteInferior = dataHora.AddHours(-3);

            var todasMesas = await _context.Mesas.ToListAsync();

            var reservasConflitantes = await _context.Reservas
                .Where(r => r.DataHoraReserva < fimPretendido && r.DataHoraReserva > limiteInferior)
                .Select(r => r.MesaId)
                .ToListAsync();

            var mesasDisponiveis = todasMesas
                .Where(m => !reservasConflitantes.Contains(m.Id))
                .Select(m => new {
                    id = m.Id,
                    numero = m.Numero,
                    capacidade = m.CapacidadePessoas
                })
                .OrderBy(m => m.numero)
                .ToList();

            return Json(mesasDisponiveis);
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmarReserva([FromBody] NovaReservaViewModel model)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            DateTime fimNova = model.DataHora.AddHours(2);
            DateTime limiteInferior = model.DataHora.AddHours(-2);

            bool conflito = await _context.Reservas.AnyAsync(r =>
                r.MesaId == model.MesaId &&
                r.DataHoraReserva < fimNova &&
                r.DataHoraReserva > limiteInferior);

            if (conflito)
                return BadRequest("Desculpe, esta mesa acabou de ser reservada para este horário. Escolha outra mesa.");

            string codigoGerado = Guid.NewGuid().ToString("N").Substring(0, 6).ToUpper();

            var novaReserva = new Reserva
            {
                UsuarioId = usuario.Id,
                MesaId = model.MesaId,
                DataHoraReserva = model.DataHora,
                CodigoConfirmacao = codigoGerado
            };

            _context.Reservas.Add(novaReserva);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                mensagem = "Reserva confirmada com sucesso!",
                codigo = codigoGerado
            });
        }

        // =========================================================
        // COLOQUE ESTE MÉTODO AQUI, ANTES DE FECHAR AS CHAVES!
        // =========================================================
        [HttpGet]
        public async Task<IActionResult> ObterMinhasReservas()
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            // Puxa as reservas do banco de dados já incluindo os dados da Mesa
            var reservas = await _context.Reservas
                .Include(r => r.Mesa)
                .Where(r => r.UsuarioId == usuario.Id)
                .OrderByDescending(r => r.DataHoraReserva)
                .ToListAsync();

            return PartialView("_MinhasReservas", reservas);
        }

        [HttpPost]
        public async Task<IActionResult> CancelarReserva(int id)
        {
            var usuario = await _userManager.GetUserAsync(User);
            if (usuario == null) return Unauthorized();

            var reserva = await _context.Reservas.FirstOrDefaultAsync(r => r.Id == id && r.UsuarioId == usuario.Id);
            if (reserva == null) return NotFound("Reserva não encontrada.");

            if (reserva.DataHoraReserva < DateTime.Now)
                return BadRequest("Não é possível cancelar uma reserva de uma data que já passou.");

            // Calcula a diferença de horas
            var horasRestantes = (reserva.DataHoraReserva - DateTime.Now).TotalHours;

            // Remove a reserva para liberar a mesa no banco
            _context.Reservas.Remove(reserva);
            await _context.SaveChangesAsync();

            // Regra da Multa de 6 horas
            if (horasRestantes < 6)
            {
                return Ok(new { mensagem = "Sua reserva foi cancelada. ATENÇÃO: Como o cancelamento foi feito com menos de 6 horas de antecedência, uma multa de R$ 50,00 foi registrada em seu nome." });
            }

            return Ok(new { mensagem = "Reserva cancelada com sucesso sem cobrança de multas." });
        }
    }
}