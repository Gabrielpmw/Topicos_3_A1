using System;
using System.Collections.Generic;

namespace restaurante.ViewModels
{
    public class RelatorioViewModel
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }

        // Faturamento
        public decimal FaturamentoPresencial { get; set; }
        public decimal FaturamentoDeliveryProprio { get; set; }
        public decimal FaturamentoDeliveryApp { get; set; }
        public decimal FaturamentoTotal => FaturamentoPresencial + FaturamentoDeliveryProprio + FaturamentoDeliveryApp;

        // Lista de Itens Vendidos
        public List<ItemVendidoViewModel> ItensMaisVendidos { get; set; } = new List<ItemVendidoViewModel>();
    }

    public class ItemVendidoViewModel
    {
        public string NomePrato { get; set; }
        public int QuantidadeTotal { get; set; }
        public int QuantidadeComoSugestao { get; set; }
        public int QuantidadeSemSugestao => QuantidadeTotal - QuantidadeComoSugestao;
    }
}