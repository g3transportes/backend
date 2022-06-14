using System;
namespace G3Transportes.WebApi.Filters
{
    public class Caminhao
    {
        public Caminhao()
        {
            this.CodigoFilter = false;
            this.MotoristaFilter = false;
            this.ProprietarioFilter = false;
            this.NomeFilter = false;
            this.PlacaFilter = false;
            this.ModeloFilter = false;
            this.CidadeFilter = false;
            this.CapacidadeFilter = false;
            this.AtivoFilter = false;
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool MotoristaFilter { get; set; }
        public int? MotoristaValue { get; set; }

        public bool ProprietarioFilter { get; set; }
        public int? ProprietarioValue { get; set; }

        public bool NomeFilter { get; set; }
        public string NomeValue { get; set; }

        public bool PlacaFilter { get; set; }
        public string PlacaValue { get; set; }

        public bool RenavamFilter { get; set; }
        public string RenavamValue { get; set; }

        public bool ModeloFilter { get; set; }
        public string ModeloValue { get; set; }

        public bool CidadeFilter { get; set; }
        public string CidadeValue { get; set; }

        public bool CapacidadeFilter { get; set; }
        public double? CapacidadeValue { get; set; }

        public bool AtivoFilter { get; set; }
        public bool AtivoValue { get; set; }
    }
}
