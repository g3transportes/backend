using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("forma-pagamento")]
    public class FormaPagamento : Controller
    {
        [HttpGet("lista")]
        public ListResult<Models.FormaPagamento> Pega()
        {
            var result = new ListResult<Models.FormaPagamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.FormaPagamento
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
        public ItemResult<Models.FormaPagamento> Pega(int id)
        {
            var result = new ItemResult<Models.FormaPagamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.FormaPagamento.FirstOrDefault(a => a.Id == id);

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
        public ItemResult<Models.FormaPagamento> Insere([FromBody]Models.FormaPagamento item)
        {
            var result = new ItemResult<Models.FormaPagamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.FormaPagamento.Add(item);
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
        public ItemResult<Models.FormaPagamento> Atualiza([FromBody]Models.FormaPagamento item)
        {
            var result = new ItemResult<Models.FormaPagamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.FormaPagamento.Update(item);
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
        public ItemResult<Models.FormaPagamento> Deleta(int id)
        {
            var result = new ItemResult<Models.FormaPagamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.FormaPagamento.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.FormaPagamento.Remove(query);
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
