using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Helpers
{
    public class FinalizaPedido
    {
        public FinalizaPedido()
        {
            this.Lancamentos = new List<Models.Lancamento>();
        }

        public Models.Pedido Pedido { get; set; }
        public List< Models.Lancamento> Lancamentos { get; set; }
    }
}
