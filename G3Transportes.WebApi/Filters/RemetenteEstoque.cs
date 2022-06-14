using System;

namespace G3Transportes.WebApi.Filters
{
    public class RemetenteEstoque
    {
        public RemetenteEstoque()
        {
            this.CodigoFilter = false;
            this.RemetenteFilter = false;
            this.PedidoFilter = false;
            this.ClienteFilter = false;
            this.TipoFilter = false;
            this.DataFilter = false;
            this.TransferenciaFilter = false;
            this.AtivoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool RemetenteFilter { get; set; }
        public int? RemetenteValue { get; set; }

        public bool PedidoFilter { get; set; }
        public int? PedidoValue { get; set; }

        public bool ClienteFilter { get; set; }
        public int? ClienteValue { get; set; }

        public bool TipoFilter { get; set; }
        public string TipoValue { get; set; }

        public bool DataFilter { get; set; }
        public DateTime? DataMinValue { get; set; }
        public DateTime? DataMaxValue { get; set; }

        public bool TransferenciaFilter { get; set; }
        public bool? TransferenciaValue { get; set; }

        public bool AtivoFilter { get; set; }
        public bool? AtivoValue { get; set; }
    }
}
