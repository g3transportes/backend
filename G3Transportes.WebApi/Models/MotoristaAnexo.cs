using System;

namespace G3Transportes.WebApi.Models
{
    public class MotoristaAnexo
    {
        public int IdMotorista { get; set; }
        public int IdAnexo { get; set; }
        public DateTime Data { get; set; }

        public virtual Motorista Motorista { get; set; }
        public virtual Anexo Anexo { get; set; }
    }
}
