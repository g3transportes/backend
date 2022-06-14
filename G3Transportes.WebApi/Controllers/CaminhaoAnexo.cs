using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("caminhao-anexo")]
    public class CaminhaoCaminhaoAnexo : Controller
    {
        private IQueryable<Models.CaminhaoAnexo> Filtra(IQueryable<Models.CaminhaoAnexo> query, Filters.CaminhaoAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.CaminhaoFilter)
                query = query.Where(a => a.IdCaminhao == filtro.CaminhaoValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.CaminhaoAnexo> Pega([FromBody] Filters.CaminhaoAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.CaminhaoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.CaminhaoAnexo
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
                                    .Select(a => new Models.CaminhaoAnexo
                                    {
                                        IdCaminhao = a.IdCaminhao,
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

        [HttpGet("item/caminhao/{idCaminhao}/anexo/{idAnexo}")]
        public ItemResult<Models.CaminhaoAnexo> Pega(int idCaminhao, int idAnexo)
        {
            var result = new ItemResult<Models.CaminhaoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.CaminhaoAnexo.FirstOrDefault(a => a.IdCaminhao == idCaminhao && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.CaminhaoAnexo> Insere([FromBody]Models.CaminhaoAnexo item)
        {
            var result = new ItemResult<Models.CaminhaoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.CaminhaoAnexo.Add(item);
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
        public ListResult<Models.CaminhaoAnexo> InsereVarios([FromBody]List<Models.CaminhaoAnexo> lista)
        {
            var result = new ListResult<Models.CaminhaoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.CaminhaoAnexo.Add(item);
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
        public ItemResult<Models.CaminhaoAnexo> Atualiza([FromBody]Models.CaminhaoAnexo item)
        {
            var result = new ItemResult<Models.CaminhaoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.CaminhaoAnexo.Update(item);
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

        [HttpDelete("deleta/caminhao/{idCaminhao}/anexo/{idAnexo}")]
        public ItemResult<Models.CaminhaoAnexo> Deleta(int idCaminhao, int idAnexo)
        {
            var result = new ItemResult<Models.CaminhaoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.CaminhaoAnexo.FirstOrDefault(a => a.IdCaminhao == idCaminhao && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.CaminhaoAnexo.Remove(query);
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
