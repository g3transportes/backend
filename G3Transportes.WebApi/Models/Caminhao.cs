using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Caminhao
    {
        public Caminhao()
        {
            this.Anexos = new List<CaminhaoAnexo>();
            this.Pedidos = new List<Pedido>();
            this.Lancamentos = new List<Lancamento>();
        }

        public int Id { get; set; }
        public int? IdMotorista { get; set; }
        public int? IdProprietario { get; set; }
        public string Nome { get; set; }
        public string Modelo { get; set; }
        public double Capacidade { get; set; }
        public bool Ativo { get; set; }

        public string Placa { get; set; }
        public string Placa2 { get; set; }
        public string Placa3 { get; set; }
        public string Placa4 { get; set; }
        public string Renavam { get; set; }
        public string Renavam2 { get; set; }
        public string Renavam3 { get; set; }
        public string Renavam4 { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }

        public virtual Motorista Motorista { get; set; }
        public virtual Proprietario Proprietario { get; set; }
        public virtual List<CaminhaoAnexo> Anexos { get; set; }
        public virtual List<Pedido> Pedidos { get; set; }
        public virtual List<Lancamento> Lancamentos { get; set; }
    }
}
