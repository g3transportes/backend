using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Motorista
    {
        public Motorista()
        {
            this.Caminhoes = new List<Caminhao>();
            this.Anexos = new List<MotoristaAnexo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Documento1 { get; set; }
        public string Documento2 { get; set; }
        public string Documento3 { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Telefone3 { get; set; }
        
        public bool Ativo { get; set; }

        public string Categoria { get; set; }
        public DateTime? DataHabilitacao { get; set; }
        public DateTime? DataVencimento { get; set; }
        public string EndRua { get; set; }
        public string EndNumero { get; set; }
        public string EndComplemento { get; set; }
        public string EndBairro { get; set; }
        public string EndCidade { get; set; }
        public string EndEstado { get; set; }
        public string EndCep { get; set; }
        public string Observacao { get; set; }

        public string BancoNome { get; set; }
        public string BancoAgencia { get; set; }
        public string BancoOperacao { get; set; }
        public string BancoConta { get; set; }
        public string BancoTitular { get; set; }
        public string BancoDocumento { get; set; }

        public virtual List<MotoristaAnexo> Anexos { get; set; }
        public virtual List<Caminhao> Caminhoes { get; set; }
    }
}
