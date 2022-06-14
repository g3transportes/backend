using System;
namespace G3Transportes.WebApi.Filters
{
    public class ClienteAnexo
    {
        public ClienteAnexo()
        {
            this.ClienteFilter = false;
            this.AnexoFilter = false;
        }

        public bool ClienteFilter { get; set; }
        public int ClienteValue { get; set; }

        public bool AnexoFilter { get; set; }
        public int AnexoValue { get; set; }
    }
}
