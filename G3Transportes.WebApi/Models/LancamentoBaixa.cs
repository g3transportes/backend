using System;
namespace G3Transportes.WebApi.Models
{
    public class LancamentoBaixa
    {
        public LancamentoBaixa()
        {
        }

        public int Id { get; set; }
        public int IdLancamento { get; set; }
        public int? IdContaBancaria { get; set; }
        public int? IdFormaPagamento { get; set; }
        public string Tipo { get; set; }
        public double Valor { get; set; }
        public DateTime Data { get; set; }
        public string Observacao { get; set; }

        public virtual Lancamento Lancamento { get; set; }
        public virtual ContaBancaria ContaBancaria { get; set; }
        public virtual FormaPagamento FormaPagamento { get; set; }
    }
}
