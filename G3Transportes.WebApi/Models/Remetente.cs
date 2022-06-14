using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Remetente
    {
        public Remetente()
        {
            this.Pedidos = new List<Pedido>();
            this.Anexos = new List<RemetenteAnexo>();
            this.Estoques = new List<RemetenteEstoque>();
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

        public int EstoqueAtual { get; set; }

        public virtual List<Pedido> Pedidos { get; set; }
        public virtual List<RemetenteAnexo> Anexos { get; set; }
        public virtual List<RemetenteEstoque> Estoques { get; set; }
    }
}
