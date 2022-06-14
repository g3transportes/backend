using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Anexo
    {
        public Anexo()
        {
            this.Caminhoes = new List<CaminhaoAnexo>();
            this.Clientes = new List<ClienteAnexo>();
            this.Motoristas = new List<MotoristaAnexo>();
            this.Pedidos = new List<PedidoAnexo>();
            this.Lancamentos = new List<LancamentoAnexo>();
            this.Proprietarios = new List<ProprietarioAnexo>();
            this.Remetentes = new List<RemetenteAnexo>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Arquivo { get; set; }

        public virtual List<CaminhaoAnexo> Caminhoes { get; set; }
        public virtual List<ClienteAnexo> Clientes { get; set; }
        public virtual List<MotoristaAnexo> Motoristas { get; set; }
        public virtual List<PedidoAnexo> Pedidos { get; set; }
        public virtual List<LancamentoAnexo> Lancamentos { get; set; }
        public virtual List<ProprietarioAnexo> Proprietarios { get; set; }
        public virtual List<RemetenteAnexo> Remetentes { get; set; }
    }
}
