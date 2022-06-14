using System;

namespace G3Transportes.WebApi.Models
{
    public class PedidoAnexo
    {
        public int IdPedido { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
