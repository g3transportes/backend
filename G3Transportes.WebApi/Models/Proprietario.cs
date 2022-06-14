using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Proprietario
    {
        public Proprietario()
        {
            this.Caminhoes = new List<Caminhao>();
            this.Anexos = new List<ProprietarioAnexo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Documento { get; set; }
        public string Documento2 { get; set; }
        public string Antt { get; set; }
        public DateTime? AnttData { get; set; }
        public string Tipo { get; set; }
        public string Pis { get; set; }
        public string Filiacao { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }

        public string EndRua { get; set; }
        public string EndNumero { get; set; }
        public string EndComplemento { get; set; }
        public string EndBairro { get; set; }
        public string EndCidade { get; set; }
        public string EndEstado { get; set; }
        public string EndCep { get; set; }

        public string BancoNome { get; set; }
        public string BancoAgencia { get; set; }
        public string BancoOperacao { get; set; }
        public string BancoConta { get; set; }
        public string BancoTitular { get; set; }
        public string BancoDocumento { get; set; }

        public string Observacao { get; set; }
        public bool Ativo { get; set; }

        public virtual List<Caminhao> Caminhoes { get; set; }
        public virtual List<ProprietarioAnexo> Anexos { get; set; }
    }
}
