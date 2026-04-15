namespace restaurante.ViewModels
{
    public class ItemCarrinhoViewModel
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }

    public class FinalizarPedidoViewModel
    {
        public List<ItemCarrinhoViewModel> Itens { get; set; } = new List<ItemCarrinhoViewModel>();
        public int TipoAtendimento { get; set; } // 0: Retirada, 1: Próprio, 2: App
        public int? EnderecoId { get; set; }
        public string? NomeApp { get; set; }
        public int MetodoPagamento { get; set; } // 0: Pix, 1: Retirada
    }
}
