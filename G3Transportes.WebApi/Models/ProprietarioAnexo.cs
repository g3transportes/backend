using System;
namespace G3Transportes.WebApi.Models
{
    public class ProprietarioAnexo
    {
        public ProprietarioAnexo()
        {
        }

        public int IdProprietario { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Proprietario Proprietario { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
