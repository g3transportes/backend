using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("cliente-anexo")]
    public class ClienteAnexo : Controller
    {
        private IQueryable<Models.ClienteAnexo> Filtra(IQueryable<Models.ClienteAnexo> query, Filters.ClienteAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.ClienteFilter)
                query = query.Where(a => a.IdCliente == filtro.ClienteValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.ClienteAnexo> Pega([FromBody] Filters.ClienteAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.ClienteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ClienteAnexo
                                .AsQueryable();

                //efetua o filtro
                query = Filtra(query, filtro);

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = pagina;
                result.PageSize = tamanho;
                result.TotalItems = query.Count();
                result.TotalPages = Comum.CalculaTotalPages(result.TotalItems, result.PageSize);
                result.Items = query.Skip(result.PageSize * (result.CurrentPage - 1))
                                    .Take(result.PageSize)
                                    .Select(a => new Models.ClienteAnexo
                                    {
                                        IdCliente = a.IdCliente,
                                        IdAnexo = a.IdAnexo,
                                        Data = a.Data.Date,
                                        Anexo = new Models.Anexo
                                        {
                                            Id = a.Anexo.Id,
                                            Nome = a.Anexo.Nome,
                                            Arquivo = a.Anexo.Arquivo
                                        }
                                    })
                                    .ToList();
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpGet("item/cliente/{idCliente}/anexo/{idAnexo}")]
        public ItemResult<Models.ClienteAnexo> Pega(int idCliente, int idAnexo)
        {
            var result = new ItemResult<Models.ClienteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ClienteAnexo.FirstOrDefault(a => a.IdCliente == idCliente && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.ClienteAnexo> Insere([FromBody]Models.ClienteAnexo item)
        {
            var result = new ItemResult<Models.ClienteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.ClienteAnexo.Add(item);
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

        [HttpPost("insere/varios")]
        public ListResult<Models.ClienteAnexo> InsereVarios([FromBody]List<Models.ClienteAnexo> lista)
        {
            var result = new ListResult<Models.ClienteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.ClienteAnexo.Add(item);
                    conn.SaveChanges();

                    //pega item incluido
                    result.Items.Add(item);
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
        public ItemResult<Models.ClienteAnexo> Atualiza([FromBody]Models.ClienteAnexo item)
        {
            var result = new ItemResult<Models.ClienteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.ClienteAnexo.Update(item);
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

        [HttpDelete("deleta/cliente/{idCliente}/anexo/{idAnexo}")]
        public ItemResult<Models.ClienteAnexo> Deleta(int idCliente, int idAnexo)
        {
            var result = new ItemResult<Models.ClienteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ClienteAnexo.FirstOrDefault(a => a.IdCliente == idCliente && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.ClienteAnexo.Remove(query);
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
