using System;
namespace G3Transportes.WebApi.Filters
{
    public class Anexo
    {
        public Anexo()
        {
            this.CodigoFilter = false;
            this.DescricaoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool DescricaoFilter { get; set; }
        public string DescricaoValue { get; set; }
    }
}
