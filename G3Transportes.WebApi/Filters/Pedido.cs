using System;
namespace G3Transportes.WebApi.Filters
{
    public class Pedido
    {
        public Pedido()
        {
            this.CodigoFilter = false;
            this.CaminhaoFilter = false;
            this.ClienteFilter = false;
            this.RemetenteFilter = false;
            this.OrdemServicoFilter = false;
            this.NumPedidoFilter = false;
            this.CTeFilter = false;
            this.NFeFilter = false;
            this.DataEmissaoFilter = false;
            this.DataColetaFilter = false;
            this.DataEntregaFilter = false;
            this.DataPagamentoFilter = false;
            this.DataFinalizadoFilter = false;
            this.DataDevolucaoFilter = false;
            this.ValorBrutoFilter = false;
            this.ValorLiquidoFilter = false;
            this.ValorFreteFilter = false;
            this.ValorComissaoFilter = false;
            this.DescricaoFilter = false;
            this.FinalizadoFilter = false;
            this.ColetadoFilter = false;
            this.EntregueFilter = false;
            this.SolicitacaoFilter = false;
            this.PagoFilter = false;
            this.DevolvidoFilter = false;
            this.AtivoFilter = false;
            this.MesFilter = false;
            this.AnoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool CaminhaoFilter { get; set; }
        public int? CaminhaoValue { get; set; }

        public bool ClienteFilter { get; set; }
        public int? ClienteValue { get; set; }

        public bool RemetenteFilter { get; set; }
        public int? RemetenteValue { get; set; }

        public bool OrdemServicoFilter { get; set; }
        public string OrdemServicoValue { get; set; }

        public bool NumPedidoFilter { get; set; }
        public string NumPedidoValue { get; set; }

        public bool CTeFilter { get; set; }
        public string CTeValue { get; set; }

        public bool NFeFilter { get; set; }
        public string NFeValue { get; set; }

        public bool DataEmissaoFilter { get; set; }
        public DateTime? DataEmissaoMinValue { get; set; }
        public DateTime? DataEmissaoMaxValue { get; set; }

        public bool DataColetaFilter { get; set; }
        public DateTime? DataColetaMinValue { get; set; }
        public DateTime? DataColetaMaxValue { get; set; }

        public bool DataEntregaFilter { get; set; }
        public DateTime? DataEntregaMinValue { get; set; }
        public DateTime? DataEntregaMaxValue { get; set; }

        public bool DataPagamentoFilter { get; set; }
        public DateTime? DataPagamentoMinValue { get; set; }
        public DateTime? DataPagamentoMaxValue { get; set; }

        public bool DataFinalizadoFilter { get; set; }
        public DateTime? DataFinalizadoMinValue { get; set; }
        public DateTime? DataFinalizadoMaxValue { get; set; }

        public bool DataDevolucaoFilter { get; set; }
        public DateTime? DataDevolucaoMinValue { get; set; }
        public DateTime? DataDevolucaoMaxValue { get; set; }

        public bool ValorBrutoFilter { get; set; }
        public double? ValorBrutoValue { get; set; }

        public bool ValorLiquidoFilter { get; set; }
        public double? ValorLiquidoValue { get; set; }

        public bool ValorFreteFilter { get; set; }
        public double? ValorFreteValue { get; set; }

        public bool ValorComissaoFilter { get; set; }
        public double? ValorComissaoValue { get; set; }

        public bool DescricaoFilter { get; set; }
        public string DescricaoValue { get; set; }

        public bool SolicitacaoFilter { get; set; }
        public bool? SolicitacaoValue { get; set; }

        public bool FinalizadoFilter { get; set; }
        public bool? FinalizadoValue { get; set; }

        public bool ColetadoFilter { get; set; }
        public bool? ColetadoValue { get; set; }

        public bool EntregueFilter { get; set; }
        public bool? EntregueValue { get; set; }

        public bool PagoFilter { get; set; }
        public bool? PagoValue { get; set; }

        public bool DevolvidoFilter { get; set; }
        public bool? DevolvidoValue { get; set; }

        public bool AtivoFilter { get; set; }
        public bool? AtivoValue { get; set; }
        
        public bool MesFilter { get; set; }
        public int? MesValue { get; set; }
        
        public bool AnoFilter { get; set; }
        public int? AnoValue { get; set; }
    }
}
