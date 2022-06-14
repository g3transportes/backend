using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("conta-bancaria")]
    public class ContaBancaria : Controller
    {
        [HttpGet("lista")]
        public ListResult<Models.ContaBancaria> Pega()
        {
            var result = new ListResult<Models.ContaBancaria>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ContaBancaria
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
        public ItemResult<Models.ContaBancaria> Pega(int id)
        {
            var result = new ItemResult<Models.ContaBancaria>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ContaBancaria.FirstOrDefault(a => a.Id == id);

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

        [HttpPost("extrato")]
        public ListResult<ViewModels.Extrato> Extrato([FromBody] ViewModels.ExtratoFiltro filtro)
        {
            var result = new ListResult<ViewModels.Extrato>();

            try
            {
                var repository = new ViewModels.ExtratoRepository();
                result = repository.Pega(filtro);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("insere")]
        public ItemResult<Models.ContaBancaria> Insere([FromBody]Models.ContaBancaria item)
        {
            var result = new ItemResult<Models.ContaBancaria>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.ContaBancaria.Add(item);
                conn.SaveChanges();

                //atualiza saldo
                Trigger_AtualizaSaldoAtual(item.Id);

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
        public ItemResult<Models.ContaBancaria> Atualiza([FromBody]Models.ContaBancaria item)
        {
            var result = new ItemResult<Models.ContaBancaria>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                conn.ContaBancaria.Update(item);
                conn.SaveChanges();

                //atualiza saldo
                Trigger_AtualizaSaldoAtual(item.Id);

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
        public ItemResult<Models.ContaBancaria> Deleta(int id)
        {
            var result = new ItemResult<Models.ContaBancaria>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.ContaBancaria.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.ContaBancaria.Remove(query);
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

        public void Trigger_AtualizaSaldoAtual(int id)
        {
            try
            {
                using var conn = new Contexts.EFContext();

                var conta = conn.ContaBancaria.FirstOrDefault(a => a.Id == id);

                if (conta != null)
                {
                    //calcula
                    conta.Debitos = conn.LancamentoBaixa.Where(a => a.IdContaBancaria == id && a.Tipo == "D").Sum(a => a.Valor);
                    conta.Creditos = conn.LancamentoBaixa.Where(a => a.IdContaBancaria == id && a.Tipo == "C").Sum(a => a.Valor);
                    conta.SaldoAtual = conta.SaldoInicial + (conta.Creditos - conta.Debitos);

                    //salva
                    conn.ContaBancaria.Update(conta);
                    conn.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
