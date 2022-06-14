using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.ViewModels
{
    public class Estoque
    {
        public Estoque()
        {
            this.Movimentacoes = new List<Models.RemetenteEstoque>();    
        }

        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int SaldoAnterior { get; set; }
        public int Creditos { get; set; }
        public int Debitos { get; set; }
        public int SaldoAtual { get; set; }
        public int? IdRemetente { get; set; }
        public string Remetente { get; set; }
        public int? IdCliente { get; set; }
        public string Cliente { get; set; }

        public List<Models.RemetenteEstoque> Movimentacoes { get; set; }
    }
}
