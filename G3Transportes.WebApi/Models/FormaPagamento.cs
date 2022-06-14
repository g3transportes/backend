using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class FormaPagamento
    {
        public FormaPagamento()
        {
            this.Lancamentos = new List<Lancamento>();
            this.Baixas = new List<LancamentoBaixa>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Ativo { get; set; }

        public virtual List<Lancamento> Lancamentos { get; set; }
        public virtual List<LancamentoBaixa> Baixas { get; set; }
    }
}
