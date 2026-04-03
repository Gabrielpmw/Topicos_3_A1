namespace restaurante.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public DateTime DataHora { get; set; }
        public decimal PrecoFinal { get; set; }
        public bool IsCancelado { get; set; }
        public Usuario Usuario { get; set; }
        public ICollection<ItemPedido> ItensPedido { get; set; }

        public int AtendimentoId { get; set; }
        public Atendimento Atendimento { get; set; }
    }
}
