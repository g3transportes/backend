using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Pedido
    {
        public Pedido()
        {
            this.Anexos = new List<PedidoAnexo>();
            this.Lancamentos = new List<Lancamento>();
            this.Estoques = new List<RemetenteEstoque>();
        }

        public int Id { get; set; }
        public int? IdCaminhao { get; set; }
        public int IdCliente { get; set; }
        public int? IdRemetente { get; set; }

        public string OrdemServico { get; set; }
        public string NumPedido { get; set; }
        public string Destinatario { get; set; }
        public string LocalColeta { get; set; }

        public DateTime DataCriacao { get; set; }
        public DateTime? DataColeta { get; set; }
        public DateTime? DataEntrega { get; set; }
        public DateTime? DataFinalizado { get; set; }
        public DateTime? DataPagamento { get; set; }
        public DateTime? DataDevolucao { get; set; }

        
        public double Quantidade { get; set; }
        public int Paletes { get; set; }
        public int PaletesDevolvidos { get; set; }
        public double ValorUnitario { get; set; }
        public double ValorBruto { get; set; }
        public double ValorLiquido { get; set; }
        public double ValorPedagio { get; set; }
        
        public double ValorAcrescimo { get; set; }
        public double ValorDesconto { get; set; }

        public double FreteUnitario { get; set; }
        public double ValorFrete { get; set; }

        public double ComissaoUnitario { get; set; }
        public double ValorComissao { get; set; }
        public double ComissaoMargem { get; set; }

        public double ValorPedagioCliente { get; set; }
        public double ValorPegadioG3 { get; set; }

        public string Observacao { get; set; }
        public bool Coletado { get; set; }
        public bool Entregue { get; set; }
        public bool Pago { get; set; }
        public bool Finalizado { get; set; }
        public bool Devolvido { get; set; }
        public bool Ativo { get; set; }

        public string CTe { get; set; }
        public string NFe { get; set; }
        public string Boleto { get; set; }


        public virtual Caminhao Caminhao { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Remetente Remetente { get; set; }
        public virtual List<PedidoAnexo> Anexos { get; set; }
        public virtual List<Lancamento> Lancamentos { get; set; }
        public virtual List<RemetenteEstoque> Estoques { get; set; }
    }
}
