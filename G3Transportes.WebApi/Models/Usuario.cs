using System;

namespace G3Transportes.WebApi.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool Ativo { get; set; }
        public bool Financeiro { get; set; }
        public bool Administrador { get; set; }
    }
}
