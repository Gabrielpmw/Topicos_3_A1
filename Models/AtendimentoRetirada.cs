namespace restaurante.Models
{
    public class AtendimentoRetirada : Atendimento
    {
        public enum StatusRetirada
        {
            EmProcesso,
            PedidoPronto
        }

        public StatusRetirada Status { get; set; }
    }
}
