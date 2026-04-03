namespace restaurante.Models
{
    public class SugestaoChefe
    {
        public int Id { get; set; }
        public int ItemCardapioId { get; set; }
        public DateTime DataSugestao { get; set; }
        public decimal DescontoAplicado { get; set; } = 0.20m;
        public ItemCardapio ItemCardapio { get; set; }
    }
}
