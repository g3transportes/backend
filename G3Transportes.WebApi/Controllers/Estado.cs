using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace G3Transportes.WebApi.Controllers
{
    [Route("estado")]
    public class Estado : Controller
    {
        [HttpGet("lista")]
        public ListResult<Models.Estado> Pega()
        {
            var result = new ListResult<Models.Estado>();

            try
            {
                using StreamReader file = System.IO.File.OpenText(@"Data/estados-cidades.json");

                JsonSerializer serializer = new JsonSerializer();
                var lista = (List<Models.Estado>)serializer.Deserialize(file, typeof(List<Models.Estado>));

                //inicializa a query
                foreach (var item in lista)
                {
                    result.Items.Add(new Models.Estado
                    {
                        Sigla = item.Sigla,
                        Nome = item.Nome,
                        Cidades = new List<string>()
                    });
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpGet("item/uf/{uf}")]
        public ItemResult<Models.Estado> Pega(string uf)
        {
            var result = new ItemResult<Models.Estado>();

            try
            {
                using StreamReader file = System.IO.File.OpenText(@"Data/estados-cidades.json");

                JsonSerializer serializer = new JsonSerializer();
                var lista = (List<Models.Estado>)serializer.Deserialize(file, typeof(List<Models.Estado>));

                //inicializa a query
                result.Item = lista.FirstOrDefault(a => a.Sigla == uf.ToUpper());
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }
    }
}
