using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("pedido-anexo")]
    public class PedidoAnexo : Controller
    {
        private IQueryable<Models.PedidoAnexo> Filtra(IQueryable<Models.PedidoAnexo> query, Filters.PedidoAnexo filtro)
        {
            //efetua verificacoes
            if (filtro.PedidoFilter)
                query = query.Where(a => a.IdPedido == filtro.PedidoValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.IdAnexo == filtro.AnexoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.PedidoAnexo> Pega([FromBody] Filters.PedidoAnexo filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.PedidoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.PedidoAnexo
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
                                    .Select(a => new Models.PedidoAnexo
                                    {
                                        IdPedido = a.IdPedido,
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

        [HttpGet("item/caminhao/{idPedido}/anexo/{idAnexo}")]
        public ItemResult<Models.PedidoAnexo> Pega(int idPedido, int idAnexo)
        {
            var result = new ItemResult<Models.PedidoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.PedidoAnexo.FirstOrDefault(a => a.IdPedido == idPedido && a.IdAnexo == idAnexo);

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
        public ItemResult<Models.PedidoAnexo> Insere([FromBody]Models.PedidoAnexo item)
        {
            var result = new ItemResult<Models.PedidoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.PedidoAnexo.Add(item);
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
        public ListResult<Models.PedidoAnexo> InsereVarios([FromBody]List<Models.PedidoAnexo> lista)
        {
            var result = new ListResult<Models.PedidoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                foreach (var item in lista)
                {
                    //inicializa a query
                    conn.PedidoAnexo.Add(item);
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
        public ItemResult<Models.PedidoAnexo> Atualiza([FromBody]Models.PedidoAnexo item)
        {
            var result = new ItemResult<Models.PedidoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.PedidoAnexo.Update(item);
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

        [HttpDelete("deleta/pedido/{idPedido}/anexo/{idAnexo}")]
        public ItemResult<Models.PedidoAnexo> Deleta(int idPedido, int idAnexo)
        {
            var result = new ItemResult<Models.PedidoAnexo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.PedidoAnexo.FirstOrDefault(a => a.IdPedido == idPedido && a.IdAnexo == idAnexo);

                if (query != null)
                {
                    conn.PedidoAnexo.Remove(query);
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
