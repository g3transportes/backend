using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("motorista-anexo")]
    public class MotoristaAnexo : Controller
    {
        private IQueryable<Models.MotoristaAnexo> Filtra(IQueryable<Models.MotoristaAnexo> query, Filters.MotoristaAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.MotoristaFilter)
                query = query.Where(a => a.IdMotorista == filtro.MotoristaValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.MotoristaAnexo> Pega([FromBody] Filters.MotoristaAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.MotoristaAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.MotoristaAnexo
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
                                    .Select(a => new Models.MotoristaAnexo
                                    {
                                        IdMotorista = a.IdMotorista,
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

        [HttpGet("item/caminhao/{idMotorista}/anexo/{idAnexo}")]
        public ItemResult<Models.MotoristaAnexo> Pega(int idMotorista, int idAnexo)
        {
            var result = new ItemResult<Models.MotoristaAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.MotoristaAnexo.FirstOrDefault(a => a.IdMotorista == idMotorista && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.MotoristaAnexo> Insere([FromBody]Models.MotoristaAnexo item)
        {
            var result = new ItemResult<Models.MotoristaAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.MotoristaAnexo.Add(item);
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
        public ListResult<Models.MotoristaAnexo> InsereVarios([FromBody]List<Models.MotoristaAnexo> lista)
        {
            var result = new ListResult<Models.MotoristaAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.MotoristaAnexo.Add(item);
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
        public ItemResult<Models.MotoristaAnexo> Atualiza([FromBody]Models.MotoristaAnexo item)
        {
            var result = new ItemResult<Models.MotoristaAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.MotoristaAnexo.Update(item);
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

        [HttpDelete("deleta/caminhao/{idMotorista}/anexo/{idAnexo}")]
        public ItemResult<Models.MotoristaAnexo> Deleta(int idMotorista, int idAnexo)
        {
            var result = new ItemResult<Models.MotoristaAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.MotoristaAnexo.FirstOrDefault(a => a.IdMotorista == idMotorista && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.MotoristaAnexo.Remove(query);
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
