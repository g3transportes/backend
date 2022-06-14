using System;
namespace G3Transportes.WebApi.Filters
{
    public class ProprietarioAnexo
    {
        public ProprietarioAnexo()
        {
            this.ProprietarioFilter = false;
            this.AnexoFilter = false;
        }

        public bool ProprietarioFilter { get; set; }
        public int ProprietarioValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
