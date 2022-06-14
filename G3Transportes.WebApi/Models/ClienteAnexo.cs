using System;

namespace G3Transportes.WebApi.Models
{
    public class ClienteAnexo
    {
        public int IdCliente { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Cliente Cliente { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
