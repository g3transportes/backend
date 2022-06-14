using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("centro-custo")]
    public class CentroCusto : Controller
    {
        [HttpGet("lista")]
        public ListResult<Models.CentroCusto> Pega()
        {
            var result = new ListResult<Models.CentroCusto>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.CentroCusto
                                .OrderBy(a => a.Tipo)
                                    .ThenBy(a => a.Referencia)
                                        .ThenBy(a => a.Nome)
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
        public ItemResult<Models.CentroCusto> Pega(int id)
        {
            var result = new ItemResult<Models.CentroCusto>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.CentroCusto.FirstOrDefault(a => a.Id == id);

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
        public ItemResult<Models.CentroCusto> Insere([FromBody]Models.CentroCusto item)
        {
            var result = new ItemResult<Models.CentroCusto>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.CentroCusto.Add(item);
                conn.SaveChanges();

                //atualiza padrao
                Trigger_AtualizaPadrao(item);

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
        public ItemResult<Models.CentroCusto> Atualiza([FromBody]Models.CentroCusto item)
        {
            var result = new ItemResult<Models.CentroCusto>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.CentroCusto.Update(item);
                conn.SaveChanges();

                //atualiza padrao
                Trigger_AtualizaPadrao(item);

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
        public ItemResult<Models.CentroCusto> Deleta(int id)
        {
            var result = new ItemResult<Models.CentroCusto>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.CentroCusto.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.CentroCusto.Remove(query);
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

        private void Trigger_AtualizaPadrao(Models.CentroCusto item)
        {
            try
            {
                if (item.Padrao == true)
                {
                    using var conn = new Contexts.EFContext();

                    var currentPadrao = conn.CentroCusto.FirstOrDefault(a => a.Tipo == item.Tipo && a.Padrao == true && a.Id != item.Id);

                    if (currentPadrao != null)
                    {
                        currentPadrao.Padrao = false;

                        //inicializa a query
                        conn.CentroCusto.Update(currentPadrao);
                        conn.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
