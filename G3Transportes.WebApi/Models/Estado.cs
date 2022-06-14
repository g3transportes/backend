using System;
using System.Collections.Generic;

namespace G3Transportes.WebApi.Models
{
    public class Estado
    {
        public Estado()
        {
            this.Cidades = new List<string>();
        }

        public string Sigla { get; set; }
        public string Nome { get; set; }
        public virtual List<string> Cidades { get; set; }
    }
}
