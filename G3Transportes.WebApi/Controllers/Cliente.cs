using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("cliente")]
    public class Cliente : Controller
    {
        private IQueryable<Models.Cliente> Filtra(IQueryable<Models.Cliente> query, Filters.Cliente filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.NomeFilter)
                query = query.Where(a => a.NomeFantasia.Contains(filtro.NomeValue) || a.RazaoSocial.Contains(filtro.NomeValue));

            if (filtro.DocumentoFilter)
                query = query.Where(a => a.Documento1.Contains(filtro.DocumentoValue) || a.Documento2.Contains(filtro.DocumentoValue));

            if (filtro.DescricaoFilter)
                query = query.Where(a => a.Observacao.Contains(filtro.DescricaoValue));

            if (filtro.AtivoFilter)
                query = query.Where(a => a.Ativo == filtro.AtivoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Cliente> Pega([FromBody] Filters.Cliente filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Cliente>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Cliente
                                .OrderBy(a => a.RazaoSocial)
                                    .ThenBy(a => a.NomeFantasia)
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
                                    .ToList();
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpGet("item/{id}")]
        public ItemResult<Models.Cliente> Pega(int id)
        {
            var result = new ItemResult<Models.Cliente>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Cliente.FirstOrDefault(a => a.Id == id);

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
        public ItemResult<Models.Cliente> Insere([FromBody]Models.Cliente item)
        {
            var result = new ItemResult<Models.Cliente>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Anexos = null;
                item.Lancamentos = null;
                item.Pedidos = null;

                //inicializa a query
                conn.Cliente.Add(item);
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
        public ItemResult<Models.Cliente> Atualiza([FromBody]Models.Cliente item)
        {
            var result = new ItemResult<Models.Cliente>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Anexos = null;
                item.Lancamentos = null;
                item.Pedidos = null;

                //inicializa a query
                conn.Cliente.Update(item);
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
        public ItemResult<Models.Cliente> Deleta(int id)
        {
            var result = new ItemResult<Models.Cliente>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Cliente.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Cliente.Remove(query);
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
