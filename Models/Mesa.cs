namespace restaurante.Models
{
    public class Mesa
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public int CapacidadePessoas { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}
