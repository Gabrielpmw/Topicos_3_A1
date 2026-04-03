namespace restaurante.Models
{
    public class Reserva
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int MesaId { get; set; }
        public DateTime DataHoraReserva { get; set; }
        public string CodigoConfirmacao { get; set; } 

        public Usuario Usuario { get; set; }
        public Mesa Mesa { get; set; }
    }
}
