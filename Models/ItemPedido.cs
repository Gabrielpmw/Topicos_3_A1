namespace restaurante.Models
{
    public class ItemPedido
    {
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        public int ItemCardapioId { get; set; }
        public ItemCardapio ItemCardapio { get; set; }

        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
