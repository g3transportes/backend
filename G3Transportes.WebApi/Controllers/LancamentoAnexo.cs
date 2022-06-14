using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace G3Transportes.WebApi.Controllers
{
    [Route("lancamento-anexo")]
    public class LancamentoAnexo : Controller
    {
        private IQueryable<Models.LancamentoAnexo> Filtra(IQueryable<Models.LancamentoAnexo> query, Filters.LancamentoAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.LancamentoFilter)
                query = query.Where(a => a.IdLancamento == filtro.LancamentoValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.LancamentoAnexo> Pega([FromBody] Filters.LancamentoAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.LancamentoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.LancamentoAnexo
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
                                    .Select(a => new Models.LancamentoAnexo
                                    {
                                        IdLancamento = a.IdLancamento,
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

        [HttpGet("item/lancamento/{idLancamento}/anexo/{idAnexo}")]
        public ItemResult<Models.LancamentoAnexo> Pega(int idLancamento, int idAnexo)
        {
            var result = new ItemResult<Models.LancamentoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.LancamentoAnexo.FirstOrDefault(a => a.IdLancamento == idLancamento && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.LancamentoAnexo> Insere([FromBody]Models.LancamentoAnexo item)
        {
            var result = new ItemResult<Models.LancamentoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.LancamentoAnexo.Add(item);
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
        public ListResult<Models.LancamentoAnexo> InsereVarios([FromBody]List<Models.LancamentoAnexo> lista)
        {
            var result = new ListResult<Models.LancamentoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.LancamentoAnexo.Add(item);
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
        public ItemResult<Models.LancamentoAnexo> Atualiza([FromBody]Models.LancamentoAnexo item)
        {
            var result = new ItemResult<Models.LancamentoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.LancamentoAnexo.Update(item);
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

        [HttpDelete("deleta/lancamento/{idLancamento}/anexo/{idAnexo}")]
        public ItemResult<Models.LancamentoAnexo> Deleta(int idLancamento, int idAnexo)
        {
            var result = new ItemResult<Models.LancamentoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.LancamentoAnexo.FirstOrDefault(a => a.IdLancamento == idLancamento && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.LancamentoAnexo.Remove(query);
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
