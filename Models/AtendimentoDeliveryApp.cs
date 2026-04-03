namespace restaurante.Models
{
    public class AtendimentoDeliveryApp : Atendimento
    {
        public StatusDelivery Status { get; set; }
        public string NomeAplicativo { get; set; }
        public int EnderecoEntregaId { get; set; }
    }
}
