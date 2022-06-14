using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.Controllers
{
    [Route("remetente-estoque")]
    public class RemetenteEstoque : Controller
    {
        private IQueryable<Models.RemetenteEstoque> Filtra(IQueryable<Models.RemetenteEstoque> query, Filters.RemetenteEstoque filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if(filtro.RemetenteFilter)
                query = query.Where(a => a.IdRemetente == filtro.RemetenteValue.Value);

            if(filtro.ClienteFilter)
                query = query.Where(a => a.Pedido.IdCliente == filtro.ClienteValue.Value);

            if(filtro.PedidoFilter)
                query = query.Where(a => a.IdPedido == filtro.PedidoValue.Value);

            if(filtro.TipoFilter)
                query = query.Where(a => a.Tipo == filtro.TipoValue);

            if (filtro.DataFilter)
            {
                if (filtro.DataMinValue == null)
                    filtro.DataMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataMaxValue == null)
                    filtro.DataMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.Data.Date >= filtro.DataMinValue.Value.Date && a.Data.Date <= filtro.DataMaxValue.Value.Date);
            }

            if (filtro.TransferenciaFilter)
                query = query.Where(a => a.Transferencia == filtro.TransferenciaValue);

            if (filtro.AtivoFilter)
                query = query.Where(a => a.Ativo == filtro.AtivoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.RemetenteEstoque> Pega([FromBody] Filters.RemetenteEstoque filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.RemetenteEstoque>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.RemetenteEstoque
                                .Include(a => a.Remetente)
                                .Include(a => a.Pedido)
                                    .ThenInclude(b => b.Cliente)
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
                                    .Select(a => new Models.RemetenteEstoque
                                    {
                                        Id = a.Id,
                                        IdRemetente = a.IdRemetente,
                                        IdPedido = a.IdPedido,
                                        Data = a.Data,
                                        Tipo = a.Tipo,
                                        Quantidade = a.Quantidade,
                                        Usuario = a.Usuario,
                                        Descricao = a.Descricao,
                                        Transferencia = a.Transferencia,
                                        Ativo = a.Ativo,
                                        Remetente = a.Remetente != null ? new Models.Remetente
                                        {
                                            Id = a.Remetente.Id,
                                            RazaoSocial = a.Remetente.RazaoSocial,
                                            NomeFantasia = a.Remetente.NomeFantasia,
                                            EstoqueAtual = a.Remetente.EstoqueAtual
                                        } : null,
                                        Pedido = a.Pedido != null ? new Models.Pedido
                                        {
                                            Id = a.Pedido.Id,
                                            OrdemServico = a.Pedido.OrdemServico,
                                            Cliente = new Models.Cliente
                                            {
                                                Id = a.Pedido.Cliente != null ? a.Pedido.Cliente.Id : 0,
                                                RazaoSocial = a.Pedido.Cliente != null ? a.Pedido.Cliente.RazaoSocial : "Não Informado",
                                                NomeFantasia = a.Pedido.Cliente != null ? a.Pedido.Cliente.NomeFantasia : "Não Informado",
                                            }
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
        public ItemResult<Models.RemetenteEstoque> Pega(int id)
        {
            var result = new ItemResult<Models.RemetenteEstoque>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.RemetenteEstoque
                                .Include(a => a.Remetente)
                                .Include(a => a.Pedido)
                                    .ThenInclude(b => b.Cliente)
                                .Select(a => new Models.RemetenteEstoque
                                {
                                    Id = a.Id,
                                    IdRemetente = a.IdRemetente,
                                    IdPedido = a.IdPedido,
                                    Data = a.Data,
                                    Tipo = a.Tipo,
                                    Quantidade = a.Quantidade,
                                    Usuario = a.Usuario,
                                    Descricao = a.Descricao,
                                    Transferencia = a.Transferencia,
                                    Ativo = a.Ativo,
                                    Remetente = a.Remetente != null ? new Models.Remetente
                                    {
                                        Id = a.Remetente.Id,
                                        RazaoSocial = a.Remetente.RazaoSocial,
                                        NomeFantasia = a.Remetente.NomeFantasia,
                                        EstoqueAtual = a.Remetente.EstoqueAtual
                                    } : null,
                                    Pedido = a.Pedido != null ? new Models.Pedido
                                    {
                                        Id = a.Pedido.Id,
                                        OrdemServico = a.Pedido.OrdemServico,
                                        Cliente = new Models.Cliente
                                        {
                                            Id = a.Pedido.Cliente != null ? a.Pedido.Cliente.Id : 0,
                                            RazaoSocial = a.Pedido.Cliente != null ? a.Pedido.Cliente.RazaoSocial : "Não Informado",
                                            NomeFantasia = a.Pedido.Cliente != null ? a.Pedido.Cliente.NomeFantasia : "Não Informado",
                                        }
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
        public ItemResult<Models.RemetenteEstoque> Insere([FromBody]Models.RemetenteEstoque item)
        {
            var result = new ItemResult<Models.RemetenteEstoque>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Remetente = null;
                item.Pedido = null;

                //inicializa a query
                conn.RemetenteEstoque.Add(item);
                conn.SaveChanges();

                //roda as triggers
                Trigger_AtualizaSaldo(item.IdRemetente);
                Trigger_AtualizaPedido(item.IdPedido);

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
        public ItemResult<Models.RemetenteEstoque> Atualiza([FromBody]Models.RemetenteEstoque item)
        {
            var result = new ItemResult<Models.RemetenteEstoque>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Pedido = null;
                item.Remetente = null;

                //inicializa a query
                conn.RemetenteEstoque.Update(item);
                conn.SaveChanges();

                //roda as triggers
                Trigger_AtualizaSaldo(item.IdRemetente);
                Trigger_AtualizaPedido(item.IdPedido);

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
        public ItemResult<Models.RemetenteEstoque> Deleta(int id)
        {
            var result = new ItemResult<Models.RemetenteEstoque>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.RemetenteEstoque.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    //efetua exclusao
                    conn.RemetenteEstoque.Remove(query);
                    conn.SaveChanges();

                    //roda as triggers
                    Trigger_AtualizaSaldo(query.IdRemetente);
                    Trigger_AtualizaPedido(query.IdPedido);

                    //pega item excluido
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
    
        [HttpPost("relatorio/estoque")]
        public ItemResult<ViewModels.Estoque> RelatorioEstoque([FromBody] Filters.RemetenteEstoque filtro)
        {
            var result = new ItemResult<ViewModels.Estoque>();

            try
            {
                using var conn = new Contexts.EFContext();
                
                //inicializa a query
                var query = conn.RemetenteEstoque
                                .Include(a => a.Remetente)
                                .Include(a => a.Pedido)
                                    .ThenInclude(b => b.Cliente)
                                .OrderBy(a => a.Data)
                                .AsQueryable();

                //efetua o filtro
                query = Filtra(query, filtro);

                //inicializa variaveis
                var lista = query.ToList();
                var creditoAnterior = 0;
                var debitoAnterior = 0;
                var saldoAnterior = 0;

                //verifica se tem remetente
                if(filtro.RemetenteValue != null)
                {
                    //data inicial
                    var dataInicio = lista.Min(a => a.Data);

                    if(dataInicio != null)
                    {
                        creditoAnterior = conn.RemetenteEstoque.Where(a => a.Data.Date < dataInicio.Date && a.IdRemetente == filtro.RemetenteValue.Value && a.Tipo == "C").Sum(a => a.Quantidade);
                        debitoAnterior = conn.RemetenteEstoque.Where(a => a.Data.Date < dataInicio.Date && a.IdRemetente == filtro.RemetenteValue.Value && a.Tipo == "D").Sum(a => a.Quantidade);
                        saldoAnterior = creditoAnterior - debitoAnterior;
                    }

                }

                //configura o resultdado
                var estoque = new ViewModels.Estoque();
                estoque.DataInicio = lista.Min(a => a.Data);
                estoque.DataFim = lista.Max(a => a.Data);
                estoque.SaldoAnterior = saldoAnterior;
                estoque.Creditos = lista.Where(a => a.Tipo == "C").Sum(a => a.Quantidade);
                estoque.Debitos = lista.Where(a => a.Tipo == "D").Sum(a => a.Quantidade);
                estoque.SaldoAtual = saldoAnterior + (estoque.Creditos - estoque.Debitos);
                estoque.IdRemetente = filtro.RemetenteValue;
                estoque.Remetente = filtro.RemetenteValue != null ? conn.Remetente.FirstOrDefault(a => a.Id == filtro.RemetenteValue.Value).RazaoSocial : "Não Informado";
                estoque.IdCliente = filtro.ClienteValue;
                estoque.Cliente = filtro.ClienteValue != null ? conn.Cliente.FirstOrDefault(a => a.Id == filtro.ClienteValue.Value).RazaoSocial : "Não Informado";
                estoque.Movimentacoes = lista;

                //seta o resultado
                result.Item = estoque;
            }
            catch (System.Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }
        
        public void Trigger_AtualizaSaldo(int idRemetente)
        {
            try
            {
                using var conn = new Contexts.EFContext();
                    
                //calaula o saldo
                var creditos = conn.RemetenteEstoque.Where(a => a.IdRemetente == idRemetente && a.Tipo == "C").Sum(a => a.Quantidade);
                var debitos = conn.RemetenteEstoque.Where(a => a.IdRemetente == idRemetente && a.Tipo == "D").Sum(a => a.Quantidade);
                var saldo = creditos - debitos;

                //recupera o remetente
                var remetente = conn.Remetente.FirstOrDefault(a => a.Id == idRemetente);
                
                if(remetente != null)
                {
                    //atualiza a quantidade da movimentacao
                    remetente.EstoqueAtual = saldo;

                    //atualiza no banco de dados
                    conn.Remetente.Update(remetente);
                    conn.SaveChanges();
                }
            }
            catch (System.Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }

        public void Trigger_AtualizaPedido(int? idPedido)
        {
            try
            {
                if(idPedido != null)
                {
                    using var conn = new Contexts.EFContext();

                    var pedido = conn.Pedido.FirstOrDefault(a => a.Id == idPedido);
                    var debitos = conn.RemetenteEstoque.Where(a => a.IdPedido == idPedido && a.Tipo == "D").Sum(a => a.Quantidade);
                    var creditos = conn.RemetenteEstoque.Where(a => a.IdPedido == idPedido && a.Tipo == "C").Sum(a => a.Quantidade);
                    var saldo = debitos - creditos;

                    if(pedido != null)
                    {
                        //atualiza a quantidade
                        pedido.PaletesDevolvidos = creditos;

                        //se saldo for zero
                        if(saldo == 0)
                        {
                            pedido.DataDevolucao = DateTime.Today;
                            pedido.Devolvido = true;
                        }
                        else
                        {
                            pedido.DataDevolucao = null;
                            pedido.Devolvido = false;
                        }

                        //atualiza o vando de dados
                        conn.Pedido.Update(pedido);
                        conn.SaveChanges();
                    }
                } 
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }
    }
}
