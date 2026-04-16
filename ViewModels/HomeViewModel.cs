namespace restaurante.ViewModels
{
    public class PratoExibicaoViewModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public decimal PrecoBase { get; set; }
        public decimal PrecoFinal { get; set; }
        public bool IsSugestao { get; set; }
        public string Ingredientes { get; set; }
        public int Periodo { get; set; } // 0 = Almoço, 1 = Jantar
        public string ImagemUrl { get; set; }
    }

    public class HomeViewModel
    {
        // Listas que vão alimentar a interface do usuário
        public List<PratoExibicaoViewModel> Sugestoes { get; set; } = new List<PratoExibicaoViewModel>();
        public List<PratoExibicaoViewModel> ItensAlmoco { get; set; } = new List<PratoExibicaoViewModel>();
        public List<PratoExibicaoViewModel> ItensJantar { get; set; } = new List<PratoExibicaoViewModel>();

        // Controles de Horário (libera ou bloqueia os botões de selecionar)
        public bool PodePedirAlmoco { get; set; }
        public bool PodePedirJantar { get; set; }
    }
}
