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
    [Route("lancamento-baixa")]
    public class LancamentoBaixa : Controller
    {
        private IQueryable<Models.LancamentoBaixa> Filtra(IQueryable<Models.LancamentoBaixa> query, Filters.LancamentoBaixa filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.LancamentoFilter)
                query = query.Where(a => a.IdLancamento == filtro.LancamentoValue);

            if (filtro.TipoFilter)
                query = query.Where(a => a.Lancamento.Tipo == filtro.TipoValue);

            if (filtro.PedidoFilter)
                query = query.Where(a => a.Lancamento.IdPedido == filtro.PedidoValue);

            if (filtro.ClienteFilter)
                query = query.Where(a => a.Lancamento.IdCliente == filtro.ClienteValue);

            if (filtro.CaminhaoFilter)
                query = query.Where(a => a.Lancamento.IdCaminhao == filtro.CaminhaoValue);

            if (filtro.ProprietarioFilter)
                query = query.Where(a => a.Lancamento.Caminhao.IdProprietario == filtro.ProprietarioValue);

            if (filtro.FormaPagamentoFilter)
                query = query.Where(a => a.IdFormaPagamento == null ? a.Lancamento.IdFormaPagamento == filtro.FormaPagamentoValue : a.IdFormaPagamento == filtro.FormaPagamentoValue);

            if (filtro.ContaBancariaFilter)
                query = query.Where(a => a.IdContaBancaria == null ? a.Lancamento.IdContaBancaria == filtro.ContaBancariaValue : a.IdContaBancaria == filtro.ContaBancariaValue);

            if (filtro.CentroCustoFilter)
                query = query.Where(a => a.Lancamento.IdCentroCusto == filtro.CentroCustoValue);

            if (filtro.TipoDocumentoFilter)
                query = query.Where(a => a.Lancamento.IdTipoDocumento == filtro.TipoDocumentoValue);

            if (filtro.FavorecidoFilter)
                query = query.Where(a => a.Lancamento.Favorecido.Contains(filtro.FavorecidoValue));

            if (filtro.EmissaoFilter)
            {
                if (filtro.EmissaoMinValue == null)
                    filtro.EmissaoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.EmissaoMaxValue == null)
                    filtro.EmissaoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.Lancamento.DataEmissao.Date >= filtro.EmissaoMinValue.Value.Date && a.Lancamento.DataEmissao.Date <= filtro.EmissaoMaxValue.Value.Date);
            }

            if (filtro.DataFilter)
            {
                if (filtro.DataMinValue == null)
                    filtro.DataMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataMaxValue == null)
                    filtro.DataMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.Data.Date >= filtro.DataMinValue.Value.Date && a.Data.Date <= filtro.DataMaxValue.Value.Date);
            }
            
            if (filtro.MesFilter)
            {
                query = query.Where(a => a.Data.Month == filtro.MesValue);
            }
            
            if (filtro.AnoFilter)
            {
                query = query.Where(a => a.Data.Year == filtro.AnoValue);
            }

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.LancamentoBaixa> Pega([FromBody] Filters.LancamentoBaixa filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.LancamentoBaixa>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.LancamentoBaixa
                                .Include(a => a.ContaBancaria)
                                .Include(a => a.FormaPagamento)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Pedido)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Cliente)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Caminhao).ThenInclude(c => c.Motorista)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Caminhao).ThenInclude(c => c.Proprietario)
                                .Include(a => a.Lancamento).ThenInclude(b => b.FormaPagamento)
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
                                    .Select(a => new Models.LancamentoBaixa
                                    {
                                        Id = a.Id,
                                        IdLancamento = a.IdLancamento,
                                        IdContaBancaria = a.IdContaBancaria,
                                        IdFormaPagamento = a.IdFormaPagamento,
                                        Tipo = a.Tipo,
                                        Data = a.Data,
                                        Valor = a.Valor,
                                        Observacao = a.Observacao,
                                        Lancamento = a.Lancamento != null ? new Models.Lancamento
                                        {
                                            Id = a.Id,
                                            IdPedido = a.Lancamento.IdPedido,
                                            IdCliente = a.Lancamento.IdCliente,
                                            IdCaminhao = a.Lancamento.IdCaminhao,
                                            IdFormaPagamento = a.Lancamento.IdFormaPagamento,
                                            Tipo = a.Lancamento.Tipo,
                                            Favorecido = a.Lancamento.Favorecido,
                                            DataEmissao = a.Lancamento.DataEmissao,
                                            DataVencimento = a.Lancamento.DataVencimento,
                                            DataBaixa = a.Lancamento.DataBaixa,
                                            ValorBruto = a.Lancamento.ValorBruto,
                                            ValorDesconto = a.Lancamento.ValorDesconto,
                                            ValorAcrescimo = a.Lancamento.ValorAcrescimo,
                                            ValorLiquido = a.Lancamento.ValorLiquido,
                                            ValorBaixado = a.Lancamento.ValorBaixado,
                                            Observacao = a.Lancamento.Observacao,
                                            Baixado = a.Lancamento.Baixado,
                                            Pedido = a.Lancamento.Pedido != null ? new Models.Pedido
                                            {
                                                Id = a.Lancamento.Pedido.Id,
                                                OrdemServico = a.Lancamento.Pedido.OrdemServico,
                                                CTe = a.Lancamento.Pedido.CTe,
                                                Boleto =  a.Lancamento.Pedido.Boleto
                                            } : null,
                                            Cliente = a.Lancamento.Cliente != null ? new Models.Cliente
                                            {
                                                Id = a.Lancamento.Cliente.Id,
                                                RazaoSocial = a.Lancamento.Cliente.RazaoSocial,
                                                NomeFantasia = a.Lancamento.Cliente.NomeFantasia,
                                                Documento1 = a.Lancamento.Cliente.Documento1,
                                                Telefone1 = a.Lancamento.Cliente.Telefone1
                                            } : null,
                                            Caminhao = a.Lancamento.Caminhao != null ? new Models.Caminhao
                                            {
                                                Id = a.Lancamento.Caminhao.Id,
                                                Placa = a.Lancamento.Caminhao.Placa,
                                                Motorista = a.Lancamento.Caminhao.Motorista != null ? new Models.Motorista
                                                {
                                                    Id = a.Lancamento.Caminhao.Motorista.Id,
                                                    Nome = a.Lancamento.Caminhao.Motorista.Nome,
                                                    Documento1 = a.Lancamento.Caminhao.Motorista.Documento1,
                                                    Telefone1 = a.Lancamento.Caminhao.Motorista.Telefone1,
                                                } : null,
                                                Proprietario = a.Lancamento.Caminhao.Proprietario != null ? new Models.Proprietario
                                                {
                                                    Id = a.Lancamento.Caminhao.Proprietario.Id,
                                                    Nome = a.Lancamento.Caminhao.Proprietario.Nome
                                                } : null
                                            } : null,
                                            FormaPagamento = a.Lancamento.FormaPagamento != null ? new Models.FormaPagamento
                                            {
                                                Id = a.Lancamento.FormaPagamento.Id,
                                                Nome = a.Lancamento.FormaPagamento.Nome
                                            } : null
                                        } : null,
                                        FormaPagamento = a.FormaPagamento != null ? new Models.FormaPagamento
                                        {
                                            Id = a.FormaPagamento.Id,
                                            Nome = a.FormaPagamento.Nome
                                        } : null,
                                        ContaBancaria = a.ContaBancaria != null ? new Models.ContaBancaria
                                        {
                                            Id = a.ContaBancaria.Id,
                                            Nome = a.ContaBancaria.Nome
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
        public ItemResult<Models.LancamentoBaixa> Pega(int id)
        {
            var result = new ItemResult<Models.LancamentoBaixa>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.LancamentoBaixa
                                .Include(a => a.ContaBancaria)
                                .Include(a => a.FormaPagamento)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Pedido)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Cliente)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Caminhao).ThenInclude(c => c.Motorista)
                                .Include(a => a.Lancamento).ThenInclude(b => b.Caminhao).ThenInclude(c => c.Proprietario)
                                .Include(a => a.Lancamento).ThenInclude(b => b.FormaPagamento)
                                .Select(a => new Models.LancamentoBaixa
                                {
                                    Id = a.Id,
                                    IdLancamento = a.IdLancamento,
                                    IdContaBancaria = a.IdContaBancaria,
                                    IdFormaPagamento = a.IdFormaPagamento,
                                    Tipo = a.Tipo,
                                    Data = a.Data,
                                    Valor = a.Valor,
                                    Observacao = a.Observacao,
                                    Lancamento = a.Lancamento != null ? new Models.Lancamento
                                    {
                                        Id = a.Id,
                                        IdPedido = a.Lancamento.IdPedido,
                                        IdCliente = a.Lancamento.IdCliente,
                                        IdCaminhao = a.Lancamento.IdCaminhao,
                                        IdFormaPagamento = a.Lancamento.IdFormaPagamento,
                                        Tipo = a.Lancamento.Tipo,
                                        Favorecido = a.Lancamento.Favorecido,
                                        DataEmissao = a.Lancamento.DataEmissao,
                                        DataVencimento = a.Lancamento.DataVencimento,
                                        DataBaixa = a.Lancamento.DataBaixa,
                                        ValorBruto = a.Lancamento.ValorBruto,
                                        ValorDesconto = a.Lancamento.ValorDesconto,
                                        ValorAcrescimo = a.Lancamento.ValorAcrescimo,
                                        ValorLiquido = a.Lancamento.ValorLiquido,
                                        ValorBaixado = a.Lancamento.ValorBaixado,
                                        Observacao = a.Lancamento.Observacao,
                                        Baixado = a.Lancamento.Baixado,
                                        Pedido = a.Lancamento.Pedido != null ? new Models.Pedido
                                        {
                                            Id = a.Lancamento.Pedido.Id,
                                            OrdemServico = a.Lancamento.Pedido.OrdemServico
                                        } : null,
                                        Cliente = a.Lancamento.Cliente != null ? new Models.Cliente
                                        {
                                            Id = a.Lancamento.Cliente.Id,
                                            RazaoSocial = a.Lancamento.Cliente.RazaoSocial,
                                            NomeFantasia = a.Lancamento.Cliente.NomeFantasia,
                                            Documento1 = a.Lancamento.Cliente.Documento1,
                                            Telefone1 = a.Lancamento.Cliente.Telefone1
                                        } : null,
                                        Caminhao = a.Lancamento.Caminhao != null ? new Models.Caminhao
                                        {
                                            Id = a.Lancamento.Caminhao.Id,
                                            Placa = a.Lancamento.Caminhao.Placa,
                                            Motorista = a.Lancamento.Caminhao.Motorista != null ? new Models.Motorista
                                            {
                                                Id = a.Lancamento.Caminhao.Motorista.Id,
                                                Nome = a.Lancamento.Caminhao.Motorista.Nome,
                                                Documento1 = a.Lancamento.Caminhao.Motorista.Documento1,
                                                Telefone1 = a.Lancamento.Caminhao.Motorista.Telefone1,
                                            } : null,
                                            Proprietario = a.Lancamento.Caminhao.Proprietario != null ? new Models.Proprietario
                                            {
                                                Id = a.Lancamento.Caminhao.Proprietario.Id,
                                                Nome = a.Lancamento.Caminhao.Proprietario.Nome
                                            } : null
                                        } : null,
                                        FormaPagamento = a.Lancamento.FormaPagamento != null ? new Models.FormaPagamento
                                        {
                                            Id = a.Lancamento.FormaPagamento.Id,
                                            Nome = a.Lancamento.FormaPagamento.Nome
                                        } : null
                                    } : null,
                                    FormaPagamento = a.FormaPagamento != null ? new Models.FormaPagamento
                                    {
                                        Id = a.FormaPagamento.Id,
                                        Nome = a.FormaPagamento.Nome
                                    } : null,
                                    ContaBancaria = a.ContaBancaria != null ? new Models.ContaBancaria
                                    {
                                        Id = a.ContaBancaria.Id,
                                        Nome = a.ContaBancaria.Nome
                                    } : null
                                })
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
        public ItemResult<Models.LancamentoBaixa> Insere([FromBody]Models.LancamentoBaixa item)
        {
            var result = new ItemResult<Models.LancamentoBaixa>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa as referencias
                item.Lancamento = null;
                item.ContaBancaria = null;
                item.FormaPagamento = null;

                //verifica periodo conciliado
                var conciliado = Trigger_VerificaConciliado(item.Data, item.IdContaBancaria.Value);

                if (conciliado != true)
                {
                    //inicializa a query
                    conn.LancamentoBaixa.Add(item);
                    conn.SaveChanges();

                    //atualiza dados do lancamento
                    Trigger_AtualizaLancamento(item.IdLancamento);

                    //atualiza saldo
                    Trigger_AtualizaSaldo(item);
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Essa conta já está conciliada para essa data");
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
        public ItemResult<Models.LancamentoBaixa> Atualiza([FromBody]Models.LancamentoBaixa item)
        {
            var result = new ItemResult<Models.LancamentoBaixa>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa as referencias
                item.Lancamento = null;
                item.ContaBancaria = null;
                item.FormaPagamento = null;

                //verifica periodo conciliado
                var conciliado = Trigger_VerificaConciliado(item.Data, item.IdContaBancaria.Value);

                if (conciliado != true)
                {
                    //inicializa a query
                    conn.LancamentoBaixa.Update(item);
                    conn.SaveChanges();

                    //atualiza dados do lancamento
                    Trigger_AtualizaLancamento(item.IdLancamento);

                    //atualiza saldo
                    Trigger_AtualizaSaldo(item);
                }
                else
                {
                    result.IsValid = false;
                    result.Errors.Add("Essa conta já está conciliada para essa data");
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
        public ItemResult<Models.LancamentoBaixa> Deleta(int id)
        {
            var result = new ItemResult<Models.LancamentoBaixa>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.LancamentoBaixa.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    //verifica periodo conciliado
                    var conciliado = Trigger_VerificaConciliado(query.Data, query.IdContaBancaria.Value);

                    if (conciliado != true)
                    {
                        conn.LancamentoBaixa.Remove(query);
                        conn.SaveChanges();

                        //atualiza dados do lancamento
                        Trigger_AtualizaLancamento(query.IdLancamento);

                        //atualiza saldo
                        Trigger_AtualizaSaldo(query);
                    }
                    else
                    {
                        result.IsValid = false;
                        result.Errors.Add("Essa conta já está conciliada para essa data");
                    }

                    //pega item incluido
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


        private void Trigger_AtualizaLancamento(int idLancamento)
        {
            try
            {
                using var conn = new Contexts.EFContext();

                var lancamento = conn.Lancamento.FirstOrDefault(a => a.Id == idLancamento);
                var baixas = conn.LancamentoBaixa.Where(a => a.IdLancamento == idLancamento);
                var pedido = lancamento.IdPedido != null ? conn.Pedido.FirstOrDefault(a => a.Id == lancamento.IdPedido) : null;

                //atualiza valores do lancamento
                lancamento.ValorBaixado = baixas.Sum(a => a.Valor);
                lancamento.ValorSaldo = lancamento.ValorLiquido - lancamento.ValorBaixado;

                if (lancamento.ValorSaldo <= 0)
                {
                    lancamento.Baixado = true;

                    //marca a data do lancamento com a data da baixa atual
                    if (baixas.Any())
                        lancamento.DataBaixa = baixas.OrderByDescending(a => a.Data).FirstOrDefault().Data;
                }
                else
                {
                    lancamento.Baixado = false;
                    lancamento.DataBaixa = null;
                }

                //salva o lancamento
                conn.Lancamento.Update(lancamento);
                conn.SaveChanges();

                //atualiza o status do pedido
                Trigger_AtualizaPedido(lancamento);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o lancamento: {0}", ex.Message));
            }
        }

        private void Trigger_AtualizaPedido(Models.Lancamento lancamento)
        {
            try
            {
                if (lancamento.Tipo == "D")
                {
                    using var conn = new Contexts.EFContext();

                    var pedido = lancamento.IdPedido != null ? conn.Pedido.FirstOrDefault(a => a.Id == lancamento.IdPedido) : null;

                    if (pedido != null)
                    {
                        pedido.Pago = lancamento.Baixado;
                        pedido.DataPagamento = lancamento.DataBaixa;

                        //salva o pedido
                        conn.Pedido.Update(pedido);
                        conn.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o lancamento: {0}", ex.Message));
            }
        }

        private void Trigger_AtualizaSaldo(Models.LancamentoBaixa item)
        {
            try
            {
                if (item.IdContaBancaria != null)
                {
                    var repository = new ContaBancaria();
                    repository.Trigger_AtualizaSaldoAtual(item.IdContaBancaria.Value);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }

        private bool Trigger_VerificaConciliado(DateTime data, int idConta)
        {
            try
            {
                var repository = new Conciliacao();
                var result = repository.VerificaExiste(data, idConta);

                return result.Item;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao verificar a consiliação", ex.Message));
            }
        }
    }
}
