using System;

namespace G3Transportes.WebApi.Models
{
    public class RemetenteEstoque
    {
        public RemetenteEstoque()
        {
            
        }

        public int Id { get; set; }
        public int IdRemetente { get; set; }
        public int? IdPedido { get; set; }
        public string Tipo { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public string Usuario { get; set; }
        public bool Transferencia { get; set; }
        public bool Ativo { get; set; }

        public Remetente Remetente { get; set; }
        public Pedido Pedido { get; set; }
    }
}