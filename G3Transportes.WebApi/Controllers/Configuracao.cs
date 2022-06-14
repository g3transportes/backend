using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("configuracao")]
    public class Configuracao : Controller
    {
        [HttpGet("item")]
        public ItemResult<Models.Configuracao> Pega()
        {
            var result = new ItemResult<Models.Configuracao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Configuracao.FirstOrDefault();

                if (query != null)
                {
                    result.Item = query;
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Nenhum registro encontrado");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPut("atualiza")]
        public ItemResult<Models.Configuracao> Atualiza([FromBody]Models.Configuracao item)
        {
            var result = new ItemResult<Models.Configuracao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.Configuracao.Update(item);
                conn.SaveChanges();

                //pega item incluido
                result.Item = item;
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
