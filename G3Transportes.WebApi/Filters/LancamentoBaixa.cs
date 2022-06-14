using System;
namespace G3Transportes.WebApi.Filters
{
    public class LancamentoBaixa
    {
        public LancamentoBaixa()
        {
            this.CodigoFilter = false;
            this.LancamentoFilter = false;
            this.TipoFilter = false;
            this.PedidoFilter = false;
            this.ClienteFilter = false;
            this.CaminhaoFilter = false;
            this.ProprietarioFilter = false;
            this.FormaPagamentoFilter = false;
            this.ContaBancariaFilter = false;
            this.CentroCustoFilter = false;
            this.TipoDocumentoFilter = false;
            this.FavorecidoFilter = false;
            this.EmissaoFilter = false;
            this.DataFilter = false;
            this.MesFilter = false;
            this.AnoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool LancamentoFilter { get; set; }
        public int? LancamentoValue { get; set; }

        public bool TipoFilter { get; set; }
        public string TipoValue { get; set; }

        public bool PedidoFilter { get; set; }
        public int? PedidoValue { get; set; }

        public bool ClienteFilter { get; set; }
        public int? ClienteValue { get; set; }

        public bool CaminhaoFilter { get; set; }
        public int? CaminhaoValue { get; set; }

        public bool ProprietarioFilter { get; set; }
        public int? ProprietarioValue { get; set; }

        public bool FormaPagamentoFilter { get; set; }
        public int? FormaPagamentoValue { get; set; }

        public bool ContaBancariaFilter { get; set; }
        public int? ContaBancariaValue { get; set; }

        public bool CentroCustoFilter { get; set; }
        public int? CentroCustoValue { get; set; }

        public bool TipoDocumentoFilter { get; set; }
        public int? TipoDocumentoValue { get; set; }

        public bool FavorecidoFilter { get; set; }
        public string FavorecidoValue { get; set; }

        public bool EmissaoFilter { get; set; }
        public DateTime? EmissaoMinValue { get; set; }
        public DateTime? EmissaoMaxValue { get; set; }

        public bool DataFilter { get; set; }
        public DateTime? DataMinValue { get; set; }
        public DateTime? DataMaxValue { get; set; }
        
        public bool MesFilter { get; set; }
        public int? MesValue { get; set; }
        
        public bool AnoFilter { get; set; }
        public int? AnoValue { get; set; }
    }
}
