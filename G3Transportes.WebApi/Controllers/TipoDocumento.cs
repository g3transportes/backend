using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("tipo-documento")]
    public class TipoDocumento : Controller
    {
        [HttpGet("lista")]
        public ListResult<Models.TipoDocumento> Pega()
        {
            var result = new ListResult<Models.TipoDocumento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.TipoDocumento
                                .OrderBy(a => a.Nome)
                                .ToList();

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = 1;
                result.PageSize = query.Count() == 0 ? 1 : query.Count();
                result.TotalItems = 1;
                result.TotalPages = Comum.CalculaTotalPages(result.TotalItems, result.PageSize);
                result.Items = query;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpGet("item/{id}")]
        public ItemResult<Models.TipoDocumento> Pega(int id)
        {
            var result = new ItemResult<Models.TipoDocumento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.TipoDocumento.FirstOrDefault(a => a.Id == id);

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

        [HttpPost("insere")]
        public ItemResult<Models.TipoDocumento> Insere([FromBody]Models.TipoDocumento item)
        {
            var result = new ItemResult<Models.TipoDocumento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.TipoDocumento.Add(item);
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

        [HttpPut("atualiza")]
        public ItemResult<Models.TipoDocumento> Atualiza([FromBody]Models.TipoDocumento item)
        {
            var result = new ItemResult<Models.TipoDocumento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.TipoDocumento.Update(item);
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

        [HttpDelete("deleta/{id}")]
        public ItemResult<Models.TipoDocumento> Deleta(int id)
        {
            var result = new ItemResult<Models.TipoDocumento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.TipoDocumento.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.TipoDocumento.Remove(query);
                    conn.SaveChanges();

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
    }
}
