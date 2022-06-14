using System;
namespace G3Transportes.WebApi.Filters
{
    public class MotoristaAnexo
    {
        public MotoristaAnexo()
        {
            this.MotoristaFilter = false;
            this.AnexoFilter = false;
        }

        public bool MotoristaFilter { get; set; }
        public int MotoristaValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
