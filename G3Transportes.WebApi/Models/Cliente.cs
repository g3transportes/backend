using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Cliente
    {
        public Cliente()
        {
            this.Anexos = new List<ClienteAnexo>();
            this.Pedidos = new List<Pedido>();   
            this.Lancamentos = new List<Lancamento>(); 
        }

        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento1 { get; set; }
        public string Documento2 { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
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

        public double ValorFreteTonelada { get; set; }
        public double ValorFreteG3 { get; set; }

        public virtual List<ClienteAnexo> Anexos { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
        public virtual List<Lancamento> Lancamentos { get; set; }
    }
}
