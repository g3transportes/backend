using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Lancamento
    {
        public Lancamento()
        {
            this.Baixas = new List<LancamentoBaixa>();
        }

        public int Id { get; set; }
        public int? IdPedido { get; set; }
        public int? IdCliente { get; set; }
        public int? IdCaminhao { get; set; }
        public int IdFormaPagamento { get; set; }
        public int? IdContaBancaria { get; set; }
        public int? IdCentroCusto { get; set; }
        public int? IdTipoDocumento { get; set; }
        public string Tipo { get; set; }
        public string Favorecido { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataBaixa { get; set; }
        public double ValorBruto { get; set; }
        public double ValorDesconto { get; set; }
        public double ValorAcrescimo { get; set; }
        public double ValorLiquido { get; set; }
        public double ValorBaixado { get; set; }
        public double ValorSaldo { get; set; }
        public string Observacao { get; set; }
        public bool Baixado { get; set; }

        public bool Autorizado { get; set; }
        public DateTime? DataAutorizacao { get; set; }
        public string UsuarioAutorizacao { get; set; }

        public string BancoNome { get; set; }
        public string BancoAgencia { get; set; }
        public string BancoOperacao { get; set; }
        public string BancoConta { get; set; }
        public string BancoTitular { get; set; }
        public string BancoDocumento { get; set; }

        public virtual Pedido Pedido { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Caminhao Caminhao { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
        public virtual ContaBancaria ContaBancaria { get; set; }
        public virtual CentroCusto CentroCusto { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }

        public virtual List<LancamentoAnexo> Anexos { get; set; }
        public virtual List<LancamentoBaixa> Baixas { get; set; }
    }
}
