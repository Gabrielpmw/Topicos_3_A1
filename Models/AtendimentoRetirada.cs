namespace restaurante.Models
{
    public class AtendimentoRetirada : Atendimento
    {
        public enum StatusRetirada
        {
            EmProcesso,
            PedidoPronto,
            Cancelado
        }

        public StatusRetirada Status { get; set; }
    }
}
    