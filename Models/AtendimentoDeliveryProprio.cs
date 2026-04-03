namespace restaurante.Models
{
    public class AtendimentoDeliveryProprio : Atendimento
    {
        public StatusDelivery Status { get; set; }
        public int EnderecoEntregaId { get; set; }
    }
}
