using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("proprietario-anexo")]
    public class ProprietarioAnexo : Controller
    {
        private IQueryable<Models.ProprietarioAnexo> Filtra(IQueryable<Models.ProprietarioAnexo> query, Filters.ProprietarioAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.ProprietarioFilter)
                query = query.Where(a => a.IdProprietario == filtro.ProprietarioValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.ProprietarioAnexo> Pega([FromBody] Filters.ProprietarioAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.ProprietarioAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ProprietarioAnexo
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
                                    .Select(a => new Models.ProprietarioAnexo
                                    {
                                        IdProprietario = a.IdProprietario,
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

        [HttpGet("item/proprietario/{idProprietario}/anexo/{idAnexo}")]
        public ItemResult<Models.ProprietarioAnexo> Pega(int idProprietario, int idAnexo)
        {
            var result = new ItemResult<Models.ProprietarioAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ProprietarioAnexo.FirstOrDefault(a => a.IdProprietario == idProprietario && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.ProprietarioAnexo> Insere([FromBody]Models.ProprietarioAnexo item)
        {
            var result = new ItemResult<Models.ProprietarioAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.ProprietarioAnexo.Add(item);
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
        public ListResult<Models.ProprietarioAnexo> InsereVarios([FromBody]List<Models.ProprietarioAnexo> lista)
        {
            var result = new ListResult<Models.ProprietarioAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.ProprietarioAnexo.Add(item);
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
        public ItemResult<Models.ProprietarioAnexo> Atualiza([FromBody]Models.ProprietarioAnexo item)
        {
            var result = new ItemResult<Models.ProprietarioAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.ProprietarioAnexo.Update(item);
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

        [HttpDelete("deleta/proprietario/{idProprietario}/anexo/{idAnexo}")]
        public ItemResult<Models.ProprietarioAnexo> Deleta(int idProprietario, int idAnexo)
        {
            var result = new ItemResult<Models.ProprietarioAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ProprietarioAnexo.FirstOrDefault(a => a.IdProprietario == idProprietario && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.ProprietarioAnexo.Remove(query);
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
