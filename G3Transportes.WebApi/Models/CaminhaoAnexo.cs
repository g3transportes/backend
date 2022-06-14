using System;

namespace G3Transportes.WebApi.Models
{
    public class CaminhaoAnexo
    {
        public int IdCaminhao { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Caminhao Caminhao { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
