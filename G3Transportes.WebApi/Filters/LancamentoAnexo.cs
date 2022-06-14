using System;
namespace G3Transportes.WebApi.Filters
{
    public class LancamentoAnexo
    {
        public LancamentoAnexo()
        {
            this.LancamentoFilter = false;
            this.AnexoFilter = false;
        }

        public bool LancamentoFilter { get; set; }
        public int LancamentoValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
