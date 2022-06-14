using System;
namespace G3Transportes.WebApi.Filters
{
    public class PedidoAnexo
    {
        public PedidoAnexo()
        {
            this.PedidoFilter = false;
            this.AnexoFilter = false;
        }

        public bool PedidoFilter { get; set; }
        public int PedidoValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
