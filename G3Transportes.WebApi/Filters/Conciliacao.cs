using System;
namespace G3Transportes.WebApi.Filters
{
    public class Conciliacao
    {
        public Conciliacao()
        {
        }

        public bool CodigoFilter { get; set; }
        public int[] CodigoValue { get; set; }

        public bool BancoFilter { get; set; }
        public int? BancoValue { get; set; }

        public bool DataFilter { get; set; }
        public DateTime? DataMinValue { get; set; }
        public DateTime? DataMaxValue { get; set; }

        public bool AnexoFilter { get; set; }
        public string AnexoValue { get; set; }
    }
}
