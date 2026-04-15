using restaurante.Models;

public class Pagamento
{
    public int Id { get; set; }
    public int PedidoId { get; set; }
    public Pedido Pedido { get; set; }

    public MetodoPagamento Metodo { get; set; }
    public bool IsPago { get; set; } = false;

    public string? ChavePixCopiaECola { get; set; }
    public DateTime? DataPagamento { get; set; }
}

public enum MetodoPagamento
{
    Pix,
    PagamentoNaRetirada
}