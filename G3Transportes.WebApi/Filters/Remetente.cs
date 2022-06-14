using System;
namespace G3Transportes.WebApi.Filters
{
    public class Remetente
    {
        public Remetente()
        {
            this.CodigoFilter = false;
            this.NomeFilter = false;
            this.DocumentoFilter = false;
            this.DescricaoFilter = false;
            this.EstoqueFilter = false;
            this.AtivoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool NomeFilter { get; set; }
        public string NomeValue { get; set; }

        public bool DocumentoFilter { get; set; }
        public string DocumentoValue { get; set; }

        public bool DescricaoFilter { get; set; }
        public string DescricaoValue { get; set; }

        public bool EstoqueFilter { get; set; }
        public bool EstoqueValue { get; set; }

        public bool AtivoFilter { get; set; }
        public bool AtivoValue { get; set; }
    }
}
