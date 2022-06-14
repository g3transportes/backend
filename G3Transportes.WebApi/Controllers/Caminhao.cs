using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("caminhao")]
    public class Caminhao : Controller
    {
        private IQueryable<Models.Caminhao> Filtra(IQueryable<Models.Caminhao> query, Filters.Caminhao filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.MotoristaFilter)
                query = query.Where(a => a.IdMotorista == filtro.MotoristaValue);

            if (filtro.ProprietarioFilter)
                query = query.Where(a => a.IdProprietario == filtro.ProprietarioValue);

            if (filtro.PlacaFilter)
                query = query.Where(a => a.Placa.Contains(filtro.PlacaValue) || a.Placa2.Contains(filtro.PlacaValue) || a.Placa3.Contains(filtro.PlacaValue) || a.Placa4.Contains(filtro.PlacaValue));

            if (filtro.RenavamFilter)
                query = query.Where(a => a.Renavam.Contains(filtro.RenavamValue) || a.Renavam2.Contains(filtro.RenavamValue) || a.Renavam3.Contains(filtro.RenavamValue) || a.Renavam4.Contains(filtro.RenavamValue));

            if (filtro.CidadeFilter)
                query = query.Where(a => a.Cidade.Contains(filtro.CidadeValue) || a.Estado.Contains(filtro.CidadeValue));

            if (filtro.CapacidadeFilter)
                query = query.Where(a => a.Capacidade >= filtro.CapacidadeValue);

            if (filtro.AtivoFilter)
                query = query.Where(a => a.Ativo == filtro.AtivoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Caminhao> Pega([FromBody] Filters.Caminhao filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Caminhao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Caminhao
                                .Include(a => a.Motorista)
                                .OrderBy(a => a.Placa)
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
                                    .Select(a => new Models.Caminhao
                                    {
                                        Id = a.Id,
                                        IdMotorista = a.IdMotorista,
                                        IdProprietario = a.IdProprietario,
                                        Nome = a.Nome,
                                        Placa = a.Placa,
                                        Placa2 = a.Placa2,
                                        Placa3 = a.Placa3,
                                        Placa4 = a.Placa4,
                                        Cidade = a.Cidade,
                                        Estado = a.Estado,
                                        Renavam = a.Renavam,
                                        Renavam2 = a.Renavam2,
                                        Renavam3 = a.Renavam3,
                                        Renavam4 = a.Renavam4,
                                        Capacidade = a.Capacidade,
                                        Modelo = a.Modelo,
                                        Ativo = a.Ativo,
                                        Motorista = a.Motorista != null ? new Models.Motorista
                                        {
                                            Id = a.Motorista.Id,
                                            Nome = a.Motorista.Nome,
                                            Documento1 = a.Motorista.Documento1
                                        } : null,
                                        Proprietario = a.Proprietario != null ? new Models.Proprietario
                                        {
                                            Id = a.Proprietario.Id,
                                            Nome = a.Motorista.Nome,
                                            Documento = a.Proprietario.Documento
                                        } : null
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
        public ItemResult<Models.Caminhao> Pega(int id)
        {
            var result = new ItemResult<Models.Caminhao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Caminhao.FirstOrDefault(a => a.Id == id);

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
        public ItemResult<Models.Caminhao> Insere([FromBody]Models.Caminhao item)
        {
            var result = new ItemResult<Models.Caminhao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa relacionamentos
                item.Motorista = null;
                item.Proprietario = null;
                item.Anexos = null;
                item.Pedidos = null;
                item.Lancamentos = null;

                //coloca a placa em uppercase
                if (!string.IsNullOrEmpty(item.Placa))
                    item.Placa = item.Placa.ToUpper().Trim();

                //verifica se ja existe caminhao com a mesma placa
                var existe = conn.Caminhao.Any(a => a.Placa == item.Placa);

                if (existe == false)
                {
                    //inicializa a query
                    conn.Caminhao.Add(item);
                    conn.SaveChanges();
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Já existe caminhão cadastrado com essa placa");
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
        public ItemResult<Models.Caminhao> Atualiza([FromBody]Models.Caminhao item)
        {
            var result = new ItemResult<Models.Caminhao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa relacionamentos
                item.Motorista = null;
                item.Proprietario = null;
                item.Anexos = null;
                item.Pedidos = null;
                item.Lancamentos = null;

                //coloca a placa em uppercase
                if (!string.IsNullOrEmpty(item.Placa))
                    item.Placa = item.Placa.ToUpper().Trim();

                //verifica se ja existe caminhao com a mesma placa
                var existe = conn.Caminhao.Any(a => a.Placa == item.Placa && a.Id != item.Id);

                if (existe == false)
                {
                    //inicializa a query
                    conn.Caminhao.Update(item);
                    conn.SaveChanges();
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Já existe caminhão cadastrado com essa placa");
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

        [HttpDelete("deleta/{id}")]
        public ItemResult<Models.Caminhao> Deleta(int id)
        {
            var result = new ItemResult<Models.Caminhao>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Caminhao.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Caminhao.Remove(query);
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
