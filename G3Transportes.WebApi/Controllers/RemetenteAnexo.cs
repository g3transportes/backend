using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("remetente-anexo")]
    public class RemetenteAnexo : Controller
    {
        private IQueryable<Models.RemetenteAnexo> Filtra(IQueryable<Models.RemetenteAnexo> query, Filters.RemetenteAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.RemetenteFilter)
                query = query.Where(a => a.IdRemetente == filtro.RemetenteValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.RemetenteAnexo> Pega([FromBody] Filters.RemetenteAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.RemetenteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.RemetenteAnexo
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
                                    .Select(a => new Models.RemetenteAnexo
                                    {
                                        IdRemetente = a.IdRemetente,
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

        [HttpGet("item/remetente/{idRemetente}/anexo/{idAnexo}")]
        public ItemResult<Models.RemetenteAnexo> Pega(int idRemetente, int idAnexo)
        {
            var result = new ItemResult<Models.RemetenteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.RemetenteAnexo.FirstOrDefault(a => a.IdRemetente == idRemetente && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.RemetenteAnexo> Insere([FromBody]Models.RemetenteAnexo item)
        {
            var result = new ItemResult<Models.RemetenteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.RemetenteAnexo.Add(item);
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
        public ListResult<Models.RemetenteAnexo> InsereVarios([FromBody]List<Models.RemetenteAnexo> lista)
        {
            var result = new ListResult<Models.RemetenteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.RemetenteAnexo.Add(item);
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
        public ItemResult<Models.RemetenteAnexo> Atualiza([FromBody]Models.RemetenteAnexo item)
        {
            var result = new ItemResult<Models.RemetenteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.RemetenteAnexo.Update(item);
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

        [HttpDelete("deleta/remetente/{idRemetente}/anexo/{idAnexo}")]
        public ItemResult<Models.RemetenteAnexo> Deleta(int idRemetente, int idAnexo)
        {
            var result = new ItemResult<Models.RemetenteAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.RemetenteAnexo.FirstOrDefault(a => a.IdRemetente == idRemetente && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.RemetenteAnexo.Remove(query);
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
