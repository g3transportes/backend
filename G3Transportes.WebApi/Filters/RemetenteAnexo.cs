using System;
namespace G3Transportes.WebApi.Filters
{
    public class RemetenteAnexo
    {
        public RemetenteAnexo()
        {
            this.RemetenteFilter = false;
            this.AnexoFilter = false;
        }

        public bool RemetenteFilter { get; set; }
        public int RemetenteValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
