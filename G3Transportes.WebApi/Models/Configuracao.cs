using System;

namespace G3Transportes.WebApi.Models
{
    public class Configuracao
    {
        public int Id { get; set; }
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        public string Documento1 { get; set; }
        public string Documento2 { get; set; }
        public string Email { get; set; }
        public string Contato { get; set; }
        public string Telefone1 { get; set; }
        public string Telefone2 { get; set; }
        public string Logomarca { get; set; }
        public string EndRua { get; set; }
        public string EndNumero { get; set; }
        public string EndComplemento { get; set; }
        public string EndBairro { get; set; }
        public string EndCidade { get; set; }
        public string EndEstado { get; set; }
        public string EndCep { get; set; }
        public string SmtpHost { get; set; }
        public string SmtpUsuario { get; set; }
        public string SmtpSenha { get; set; }
        public int SmtpPorta { get; set; }
        public bool SmtpSSL { get; set; }
    }
}
