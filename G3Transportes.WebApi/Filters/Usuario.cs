using System;
namespace G3Transportes.WebApi.Filters
{
    public class Usuario
    {
        public Usuario()
        {
            this.CodigoFilter = false;
            this.NomeFilter = false;
            this.EmailFilter = false;
            this.SenhaFilter = false;
            this.AtivoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool NomeFilter { get; set; }
        public string NomeValue { get; set; }

        public bool EmailFilter { get; set; }
        public string EmailValue { get; set; }

        public bool SenhaFilter { get; set; }
        public string SenhaValue { get; set; }

        public bool AtivoFilter { get; set; }
        public bool AtivoValue { get; set; }
    }
}
