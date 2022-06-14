using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Controllers
{
    [Route("conciliacao")]
    public class Conciliacao : Controller
    {
        private IQueryable<Models.Conciliacao> Filtra(IQueryable<Models.Conciliacao> query, Filters.Conciliacao filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.BancoFilter)
                query = query.Where(a => a.IdConta == filtro.BancoValue);

            if (filtro.AnexoFilter)
                query = query.Where(a => a.Anexo.Contains(filtro.AnexoValue));

            if (filtro.DataFilter)
            {
                if (filtro.DataMinValue == null)
                    filtro.DataMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataMaxValue == null)
                    filtro.DataMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.Data >= filtro.DataMinValue.Value.Date && a.Data.Date <= filtro.DataMaxValue.Value.Date);
            }

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Conciliacao> Pega([FromBody] Filters.Conciliacao filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Conciliacao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Conciliacao
                                .Include(a => a.Conta)
                                .OrderBy(a => a.Data)
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
                                    .Select(a => new Models.Conciliacao
                                    {
                                        Id = a.Id,
                                        IdConta = a.IdConta,
                                        Anexo = a.Anexo,
                                        Data = a.Data,
                                        Saldo = a.Saldo,
                                        Conta = a.Conta != null ? new Models.ContaBancaria
                                        {
                                            Id = a.Conta.Id,
                                            Nome = a.Conta.Nome
                                        } : null,
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

        [HttpGet("item/{id}")]
        public ItemResult<Models.Conciliacao> Pega(int id)
        {
            var result = new ItemResult<Models.Conciliacao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Conciliacao
                                .Include(a => a.Conta)
                                .FirstOrDefault(a => a.Id == id);

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
        public ItemResult<Models.Conciliacao> Insere([FromBody] Models.Conciliacao item)
        {
            var result = new ItemResult<Models.Conciliacao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa relacionamentos
                item.Conta = null;

                //verifica se ja existe consiliacao nessa data
                var existe = conn.Conciliacao.Any(a => a.Data.Date == item.Data.Date && a.IdConta == item.IdConta);

                if (existe == false)
                {
                    //inicializa a query
                    conn.Conciliacao.Add(item);
                    conn.SaveChanges();
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Já existe conciliacao feita nessa data para essa conta");
                }

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
        public ItemResult<Models.Conciliacao> Atualiza([FromBody] Models.Conciliacao item)
        {
            var result = new ItemResult<Models.Conciliacao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa relacionamentos
                item.Conta = null;

                //inicializa a query
                conn.Conciliacao.Update(item);
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
        public ItemResult<Models.Conciliacao> Deleta(int id)
        {
            var result = new ItemResult<Models.Conciliacao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Conciliacao.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Conciliacao.Remove(query);
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

        public ItemResult<bool> VerificaExiste(DateTime data, int idConta)
        {
            var result = new ItemResult<bool>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Conciliacao.Where(a => a.Data.Date >= data.Date && a.IdConta == idConta);
 
                //configura o resultado
                result.Item = query.Any();
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
