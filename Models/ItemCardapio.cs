namespace restaurante.Models
{
    public class ItemCardapio
    {
        public enum PeriodoCardapio
        {
            Almoco,
            Jantar
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoBase { get; set; }
        public PeriodoCardapio Periodo { get; set; }
        public ICollection<Ingrediente> Ingredientes { get; set; }
    }
}
