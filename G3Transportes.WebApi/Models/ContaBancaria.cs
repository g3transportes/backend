using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class ContaBancaria
    {
        public ContaBancaria()
        {
            this.Lancamentos = new List<Lancamento>();
            this.Baixas = new List<LancamentoBaixa>();
            this.Conciliacoes = new List<Conciliacao>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Banco { get; set; }
        public string Agencia { get; set; }
        public string Operacao { get; set; }
        public string Conta { get; set; }
        public string Titular { get; set; }
        public string Documento { get; set; }
        public string Observacao { get; set; }
        public double SaldoInicial { get; set; }
        public double SaldoAtual { get; set; }
        public double Creditos { get; set; }
        public double Debitos { get; set; }
        public bool Ativo { get; set; }

        public virtual List<Lancamento> Lancamentos { get; set; }
        public virtual List<LancamentoBaixa> Baixas { get; set; }
        public virtual List<Conciliacao> Conciliacoes { get; set; }
    }
}
