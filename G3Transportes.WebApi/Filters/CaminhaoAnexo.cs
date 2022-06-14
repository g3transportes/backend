using System;
namespace G3Transportes.WebApi.Filters
{
    public class CaminhaoAnexo
    {
        public CaminhaoAnexo()
        {
            this.CaminhaoFilter = false;
            this.AnexoFilter = false;
        }

        public bool CaminhaoFilter { get; set; }
        public int CaminhaoValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
