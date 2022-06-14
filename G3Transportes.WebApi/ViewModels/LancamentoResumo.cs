using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.ViewModels
{
    public class LancamentoResumo
    {
        public LancamentoResumo()
        {
            this.ItemsPagar = new List<LancamentoResumoItem>();
            this.ItemsPago = new List<LancamentoResumoItem>();
            this.ItemsReceber = new List<LancamentoResumoItem>();
            this.ItemsRecebido = new List<LancamentoResumoItem>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Referencia { get; set; }
        public DateTime? EmissaoInicio { get; set; }
        public DateTime? EmissaoFim { get; set; }
        public DateTime? VencimentoInicio { get; set; }
        public DateTime? VencimentoFim { get; set; }
        public double TotalPagar { get; set; }
        public double TotalPago { get; set; }
        public double TotalReceber { get; set; }
        public double TotalRecebido { get; set; }
        
        public List<LancamentoResumoItem> ItemsPagar { get; set; }
        public List<LancamentoResumoItem> ItemsPago { get; set; }
        public List<LancamentoResumoItem> ItemsReceber { get; set; }
        public List<LancamentoResumoItem> ItemsRecebido { get; set; }
    }

    public class LancamentoResumoItem
    {
        public LancamentoResumoItem()
        {

        }

        public int Id { get; set; }
        public int? IdPedido { get; set; }
        public string Tipo { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? Coleta { get; set; }
        public DateTime? Entrega { get; set; }
        public DateTime? Vencimento { get; set; }
        public DateTime? Baixa { get; set; }
        public double Valor { get; set; }
        public double ValorBaixado { get; set; }
        public double ValorSaldo { get; set; }
        public string CentroCusto { get; set; }
        public string TipoDocumento { get; set; }
        public string ContaBancaria { get; set; }
        public string FormaPagamento { get; set; }
        public string OrdemServico { get; set; }
        public string NumPedido { get; set; }
        public string Cliente { get; set; }
        public string Proprietario { get; set; }
        public string Motorista { get; set; }
        public string Favorecido { get; set; }
        public string Remetente { get; set; }
        public string LocalColeta { get; set; }
        public string Cte { get; set; }
        public string Nfe { get; set; }
    }

}
