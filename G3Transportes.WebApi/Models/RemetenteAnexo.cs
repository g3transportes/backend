using System;
namespace G3Transportes.WebApi.Models
{
    public class RemetenteAnexo
    {
        public RemetenteAnexo()
        {
        }

        public int IdRemetente { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Remetente Remetente { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
