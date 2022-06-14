using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ClosedXML.Excel;
using G3Transportes.WebApi.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace G3Transportes.WebApi.Controllers
{
    [Route("pedido")]
    public class Pedido : Controller
    {
        private IWebHostEnvironment _enviroment;

        public Pedido(IWebHostEnvironment environment)
        {
            _enviroment = environment;
        }

        private IQueryable<Models.Pedido> Filtra(IQueryable<Models.Pedido> query, Filters.Pedido filtro)
        {
            //efetua verificacoes
            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.CaminhaoFilter)
                query = query.Where(a => a.IdCaminhao == filtro.CaminhaoValue);

            if (filtro.ClienteFilter)
                query = query.Where(a => a.IdCliente == filtro.ClienteValue);

            if (filtro.RemetenteFilter)
                query = query.Where(a => a.IdRemetente == filtro.RemetenteValue);

            if (filtro.OrdemServicoFilter)
                query = query.Where(a => a.OrdemServico.Contains(filtro.OrdemServicoValue) ||
                                         a.NumPedido.Contains(filtro.OrdemServicoValue) ||
                                         a.CTe.Contains(filtro.OrdemServicoValue) ||
                                         a.NFe.Contains(filtro.OrdemServicoValue) ||
                                         a.Boleto.Contains(filtro.OrdemServicoValue));

            if (filtro.NumPedidoFilter)
                query = query.Where(a => a.NumPedido.Contains(filtro.NumPedidoValue));

            if (filtro.CTeFilter)
                query = query.Where(a => a.CTe.Contains(filtro.CTeValue));

            if (filtro.NFeFilter)
                query = query.Where(a => a.NFe.Contains(filtro.NFeValue));

            if (filtro.DataEmissaoFilter)
            {
                if (filtro.DataEmissaoMinValue == null)
                    filtro.DataEmissaoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataEmissaoMaxValue == null)
                    filtro.DataEmissaoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataCriacao.Date >= filtro.DataEmissaoMinValue.Value.Date && a.DataCriacao.Date <= filtro.DataEmissaoMaxValue.Value.Date);
            }

            if (filtro.DataColetaFilter)
            {
                if (filtro.DataColetaMinValue == null)
                    filtro.DataColetaMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataColetaMaxValue == null)
                    filtro.DataColetaMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataColeta.Value.Date >= filtro.DataColetaMinValue.Value.Date && a.DataColeta.Value.Date <= filtro.DataColetaMaxValue.Value.Date);
            }

            if (filtro.DataEntregaFilter)
            {
                if (filtro.DataEntregaMinValue == null)
                    filtro.DataEntregaMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataEntregaMaxValue == null)
                    filtro.DataEntregaMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataEntrega.Value.Date >= filtro.DataEntregaMinValue.Value.Date && a.DataEntrega.Value.Date <= filtro.DataEntregaMaxValue.Value.Date);
            }

            if (filtro.DataPagamentoFilter)
            {
                if (filtro.DataPagamentoMinValue == null)
                    filtro.DataPagamentoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataPagamentoMaxValue == null)
                    filtro.DataPagamentoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataPagamento.Value.Date >= filtro.DataPagamentoMinValue.Value.Date && a.DataPagamento.Value.Date <= filtro.DataPagamentoMaxValue.Value.Date);
            }

            if (filtro.DataFinalizadoFilter)
            {
                if (filtro.DataFinalizadoMinValue == null)
                    filtro.DataFinalizadoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataFinalizadoMaxValue == null)
                    filtro.DataFinalizadoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataFinalizado.Value.Date >= filtro.DataFinalizadoMinValue.Value.Date && a.DataFinalizado.Value.Date <= filtro.DataFinalizadoMaxValue.Value.Date);
            }

            if (filtro.DataDevolucaoFilter)
            {
                if (filtro.DataDevolucaoMinValue == null)
                    filtro.DataDevolucaoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.DataDevolucaoMaxValue == null)
                    filtro.DataDevolucaoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataDevolucao.Value.Date >= filtro.DataDevolucaoMinValue.Value.Date && a.DataDevolucao.Value.Date <= filtro.DataDevolucaoMaxValue.Value.Date);
            }

            if (filtro.ValorLiquidoFilter)
                query = query.Where(a => a.ValorLiquido == filtro.ValorLiquidoValue);

            if (filtro.ValorFreteFilter)
                query = query.Where(a => a.ValorFrete == filtro.ValorFreteValue);

            if (filtro.ValorComissaoFilter)
                query = query.Where(a => a.ValorComissao == filtro.ValorComissaoValue);

            if (filtro.DescricaoFilter)
                query = query.Where(a => a.Observacao.Contains(filtro.DescricaoValue));

            if (filtro.FinalizadoFilter)
                query = query.Where(a => a.Finalizado == filtro.FinalizadoValue);

            if (filtro.ColetadoFilter)
                query = query.Where(a => a.Coletado == filtro.ColetadoValue);

            if (filtro.EntregueFilter)
                query = query.Where(a => a.Entregue == filtro.EntregueValue);

            if (filtro.PagoFilter)
                query = query.Where(a => a.Pago == filtro.PagoValue);

            if (filtro.DevolvidoFilter)
                query = query.Where(a => a.Devolvido == filtro.DevolvidoValue);

            if (filtro.SolicitacaoFilter)
            {
                if(filtro.SolicitacaoValue == true)
                    query = query.Where(a => a.IdCaminhao == null);
                else
                    query = query.Where(a => a.IdCaminhao != null);
            }

            if (filtro.MesFilter)
            {
                query = query.Where(a => a.DataColeta.Value.Month == filtro.MesValue);
            }
            
            if (filtro.AnoFilter)
            {
                query = query.Where(a => a.DataColeta.Value.Year == filtro.AnoValue);
            }

            if (filtro.AtivoFilter)
                query = query.Where(a => a.Ativo == filtro.AtivoValue);

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Pedido> Pega([FromBody] Filters.Pedido filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Pedido
                                .Include(a => a.Caminhao)
                                    .ThenInclude(b => b.Motorista)
                                .Include(a => a.Cliente)
                                .OrderByDescending(a => a.DataCriacao)
                                    .ThenByDescending(a => a.Id)
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
                                    .Select(a => new Models.Pedido
                                    {
                                        Id = a.Id,
                                        IdCaminhao = a.IdCaminhao,
                                        IdCliente = a.IdCliente,
                                        IdRemetente = a.IdRemetente,
                                        OrdemServico = a.OrdemServico,
                                        NumPedido = a.NumPedido,
                                        Destinatario = a.Destinatario,
                                        LocalColeta = a.LocalColeta,
                                        DataCriacao = a.DataCriacao,
                                        DataColeta = a.DataColeta,
                                        DataFinalizado = a.DataFinalizado,
                                        DataEntrega = a.DataEntrega,
                                        DataPagamento = a.DataPagamento,
                                        DataDevolucao = a.DataDevolucao,
                                        Paletes = a.Paletes,
                                        PaletesDevolvidos = a.PaletesDevolvidos,
                                        Quantidade = a.Quantidade,
                                        ValorUnitario = a.ValorUnitario,
                                        ValorBruto = a.ValorBruto,
                                        ValorFrete = a.ValorFrete,
                                        ValorComissao = a.ValorComissao,
                                        ValorLiquido = a.ValorLiquido,
                                        ValorPedagio = a.ValorPedagio,
                                        ValorAcrescimo = a.ValorAcrescimo,
                                        ValorDesconto = a.ValorDesconto,
                                        FreteUnitario = a.FreteUnitario,
                                        ComissaoUnitario = a.ComissaoUnitario,
                                        ComissaoMargem = a.ComissaoMargem,
                                        Observacao = a.Observacao,
                                        Finalizado = a.Finalizado,
                                        Coletado = a.Coletado,
                                        Entregue = a.Entregue,
                                        Pago = a.Pago,
                                        Devolvido = a.Devolvido,
                                        Ativo = a.Ativo,
                                        CTe = a.CTe,
                                        NFe = a.NFe,
                                        Boleto = a.Boleto,
                                        ValorPedagioCliente = a.ValorPedagioCliente,
                                        ValorPegadioG3 = a.ValorPegadioG3,
                                        Cliente = a.Cliente != null ? new Models.Cliente
                                        {
                                            Id = a.Cliente.Id,
                                            RazaoSocial = a.Cliente.RazaoSocial,
                                            NomeFantasia = a.Cliente.NomeFantasia,
                                            Documento1 = a.Cliente.Documento1,
                                            Telefone1 = a.Cliente.Telefone1,
                                            ValorFreteTonelada = a.Cliente.ValorFreteTonelada,
                                            ValorFreteG3 = a.Cliente.ValorFreteG3
                                        } : null,
                                        Caminhao = a.Caminhao != null ? new Models.Caminhao
                                        {
                                            Id = a.Caminhao.Id,
                                            Placa = a.Caminhao.Placa,
                                            Capacidade = a.Caminhao.Capacidade,
                                            Motorista = new Models.Motorista
                                            {
                                                Id = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Id : 0,
                                                Nome = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Nome : "Não Informado",
                                                Documento1 = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Documento1 : "",
                                                Telefone1 = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Telefone1 : ""
                                            }
                                        } : null,
                                        Remetente = a.Remetente != null ? new Models.Remetente
                                        {
                                            Id = a.Remetente.Id,
                                            RazaoSocial = a.Remetente.RazaoSocial,
                                            NomeFantasia = a.Remetente.NomeFantasia,
                                            Documento1 = a.Remetente.Documento1,
                                            Telefone1 = a.Remetente.Telefone1
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
        public ItemResult<Models.Pedido> Pega(int id)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Pedido
                                .Select(a => new Models.Pedido
                                {
                                    Id = a.Id,
                                    IdCaminhao = a.IdCaminhao,
                                    IdCliente = a.IdCliente,
                                    IdRemetente = a.IdRemetente,
                                    OrdemServico = a.OrdemServico,
                                    NumPedido = a.NumPedido,
                                    Destinatario = a.Destinatario,
                                    LocalColeta = a.LocalColeta,
                                    DataCriacao = a.DataCriacao,
                                    DataColeta = a.DataColeta,
                                    DataFinalizado = a.DataFinalizado,
                                    DataEntrega = a.DataEntrega,
                                    DataPagamento = a.DataPagamento,
                                    DataDevolucao = a.DataDevolucao,
                                    Paletes = a.Paletes,
                                    PaletesDevolvidos = a.PaletesDevolvidos,
                                    Quantidade = a.Quantidade,
                                    ValorUnitario = a.ValorUnitario,
                                    ValorBruto = a.ValorBruto,
                                    ValorFrete = a.ValorFrete,
                                    ValorComissao = a.ValorComissao,
                                    ValorLiquido = a.ValorLiquido,
                                    ValorPedagio = a.ValorPedagio,
                                    ValorAcrescimo = a.ValorAcrescimo,
                                    ValorDesconto = a.ValorDesconto,
                                    FreteUnitario = a.FreteUnitario,
                                    ComissaoUnitario = a.ComissaoUnitario,
                                    ComissaoMargem = a.ComissaoMargem,
                                    Observacao = a.Observacao,
                                    Finalizado = a.Finalizado,
                                    Coletado = a.Coletado,
                                    Entregue = a.Entregue,
                                    Pago = a.Pago,
                                    Devolvido = a.Devolvido,
                                    Ativo = a.Ativo,
                                    CTe = a.CTe,
                                    NFe = a.NFe,
                                    Boleto = a.Boleto,
                                    ValorPedagioCliente = a.ValorPedagioCliente,
                                    ValorPegadioG3 = a.ValorPegadioG3,
                                    Cliente = a.Cliente != null ? new Models.Cliente
                                    {
                                        Id = a.Cliente.Id,
                                        RazaoSocial = a.Cliente.RazaoSocial,
                                        NomeFantasia = a.Cliente.NomeFantasia,
                                        Documento1 = a.Cliente.Documento1,
                                        Telefone1 = a.Cliente.Telefone1,
                                        ValorFreteTonelada = a.Cliente.ValorFreteTonelada,
                                        ValorFreteG3 = a.Cliente.ValorFreteG3
                                    } : null,
                                    Caminhao = a.Caminhao != null ? new Models.Caminhao
                                    {
                                        Id = a.Caminhao.Id,
                                        Placa = a.Caminhao.Placa,
                                        Capacidade = a.Caminhao.Capacidade,
                                        Motorista = a.Caminhao.Motorista != null ? new Models.Motorista
                                        {
                                            Id = a.Caminhao.Motorista.Id,
                                            Nome = a.Caminhao.Motorista.Nome,
                                            Documento1 = a.Caminhao.Motorista.Documento1,
                                            Telefone1 = a.Caminhao.Motorista.Telefone1
                                        } : null
                                    } : null,
                                    Remetente = a.Remetente != null ? new Models.Remetente
                                    {
                                        Id = a.Remetente.Id,
                                        RazaoSocial = a.Remetente.RazaoSocial,
                                        NomeFantasia = a.Remetente.NomeFantasia,
                                        Documento1 = a.Remetente.Documento1,
                                        Telefone1 = a.Remetente.Telefone1
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

        [HttpPost("ordemservico")]
        public ItemResult<string> OrdemServico([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<string>();

            try
            {
                using var conn = new Contexts.EFContext();

                if (item.IdCaminhao != 0)
                {
                    var caminhao = conn.Caminhao.FirstOrDefault(a => a.Id == item.IdCaminhao);
                    var pedido = conn.Pedido.Where(a => a.DataCriacao.Date == item.DataCriacao.Date).Count();
                    var data = item.DataCriacao.Date;

                    //configura a ordem de servico
                    result.Item = string.Format("{0}-{1}{2}{3}/{4}",
                        caminhao.Placa,
                        data.Year,
                        data.Month.ToString().PadLeft(2, '0'),
                        data.Day.ToString().PadLeft(2, '0'),
                        (pedido + 1).ToString().PadLeft(3, '0'));
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
        public ItemResult<Models.Pedido> Insere([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                
                //inicializa a query
                conn.Pedido.Add(item);
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

        [HttpPost("finaliza")]
        public ItemResult<Models.Pedido> Finaliza([FromBody]Helpers.FinalizaPedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //cria conta a receber e receber
                foreach (var lanc in item.Lancamentos)
                {
                    conn.Lancamento.Add(lanc);
                    conn.SaveChanges();
                }

                //limpa referencias
                item.Pedido.Caminhao = null;
                item.Pedido.Cliente = null;
                item.Pedido.Remetente = null;
                item.Pedido.Anexos = null;
                item.Pedido.Lancamentos = null;
                item.Pedido.Estoques = null;

                //atualiza pedido
                item.Pedido.DataFinalizado = item.Pedido.DataFinalizado == null ? DateTime.Today : item.Pedido.DataFinalizado.Value.Date;
                item.Pedido.DataColeta = item.Pedido.DataColeta == null ? DateTime.Today : item.Pedido.DataColeta.Value.Date;
                item.Pedido.Finalizado = true;
                item.Pedido.Coletado = true;

                //verifica paletes
                if (item.Pedido.Paletes == 0)
                    item.Pedido.Devolvido = true;
                else
                    item.Pedido.Devolvido = false;

                //inicializa a query
                conn.Pedido.Update(item.Pedido);
                conn.SaveChanges();

                //triggers
                Trigger_InsereEstoque(item.Pedido);

                //pega item incluido
                var pedido = conn.Pedido.FirstOrDefault(a => a.Id == item.Pedido.Id);
                pedido.Caminhao = null;
                pedido.Cliente = null;
                pedido.Anexos = null;
                pedido.Lancamentos = null;

                result.Item = pedido;
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPut("atualiza")]
        public ItemResult<Models.Pedido> Atualiza([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                //inicializa a query
                conn.Pedido.Update(item);
                conn.SaveChanges();

                //processa triggers
                Trigger_InsereEstoque(item);
                Trigger_AtualizaFinanceiro(item);
                //Trigger_AtualizaEstoque(item);

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

        [HttpPut("entrega")]
        public ItemResult<Models.Pedido> Entrega([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                //atualiza pedido
                item.DataEntrega = item.DataEntrega == null ? DateTime.Today : item.DataEntrega.Value.Date;
                item.Entregue = true;

                //inicializa a query
                conn.Pedido.Update(item);
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

        [HttpPut("estorna")]
        public ItemResult<Models.Pedido> Estorna([FromBody] Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                //efetua a validacao
                var validacao = Valida_Estorno(item);

                if (validacao.IsValid)
                {
                    //atualiza pedido
                    item.DataFinalizado = null;
                    item.DataColeta = null;
                    item.DataEntrega = null;

                    item.Finalizado = false;
                    item.Coletado = false;
                    item.Entregue = false;

                    //inicializa a query
                    conn.Pedido.Update(item);
                    conn.SaveChanges();

                    //exclui lancamentos financeiros
                    Trigger_ExcluiFinanceiro(item);
                    Trigger_ExcluiEstoque(item);
                }
                else
                {
                    result.IsValid = validacao.IsValid;
                    result.Errors = validacao.Errors;
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

        [HttpPut("paga")]
        public ItemResult<Models.Pedido> Paga([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                //atualiza pedido
                item.DataPagamento = item.DataPagamento == null ? DateTime.Today : item.DataPagamento.Value.Date;
                item.Pago = true;

                //inicializa a query
                conn.Pedido.Update(item);
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
        
        [HttpPut("devolve")]
        public ItemResult<Models.Pedido> Devolve([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                //atualiza pedido
                item.DataDevolucao = DateTime.Today;
                item.Devolvido = true;

                //inicializa a query
                conn.Pedido.Update(item);
                conn.SaveChanges();

                //processa a trigger


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

        [HttpPut("estorna/devolucao")]
        public ItemResult<Models.Pedido> EstornaDevolcucao([FromBody]Models.Pedido item)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa referencias
                item.Caminhao = null;
                item.Cliente = null;
                item.Remetente = null;
                item.Anexos = null;
                item.Lancamentos = null;
                item.Estoques = null;

                //atualiza pedido
                item.DataDevolucao = null;
                item.Devolvido = false;

                //inicializa a query
                conn.Pedido.Update(item);
                conn.SaveChanges();

                //processa a trigger


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
        public ItemResult<Models.Pedido> Deleta(int id)
        {
            var result = new ItemResult<Models.Pedido>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Pedido.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Pedido.Remove(query);
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

        [HttpPost("exporta")]
        public ItemResult<string> Exporta([FromBody] Filters.Pedido filtro)
        {
            var result = new ItemResult<string>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Pedido
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.Cliente)
                                .Include(a => a.Remetente)
                                .OrderBy(a => a.DataCriacao)
                                    .ThenBy(a => a.Id)
                                .AsQueryable();

                //efetua o filtro
                query = Filtra(query, filtro);


                //cria arquivo excel
                using (var workbook = new XLWorkbook())
                {
                    var letters = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "X", "Y", "Z" };
                    var worksheet = workbook.Worksheets.Add("OrdemServico");

                    //cria cabecalhos
                    var currentRow = 1;
                    worksheet.Cell(currentRow, 1).Value = "Ordem de Servico";
                    worksheet.Cell(currentRow, 2).Value = "Num. Pedido";
                    worksheet.Cell(currentRow, 3).Value = "Data Criacao";
                    worksheet.Cell(currentRow, 4).Value = "Data Coleta";
                    worksheet.Cell(currentRow, 5).Value = "Data Entrega";
                    worksheet.Cell(currentRow, 6).Value = "Cliente";
                    worksheet.Cell(currentRow, 7).Value = "Caminhão";
                    worksheet.Cell(currentRow, 8).Value = "Motorista";
                    worksheet.Cell(currentRow, 9).Value = "Proprietario";
                    worksheet.Cell(currentRow, 10).Value = "Remetente";
                    worksheet.Cell(currentRow, 11).Value = "Local da Coleta";
                    worksheet.Cell(currentRow, 12).Value = "CTe";
                    worksheet.Cell(currentRow, 13).Value = "NFe";
                    worksheet.Cell(currentRow, 14).Value = "Paletes";
                    worksheet.Cell(currentRow, 15).Value = "Quantidade";
                    worksheet.Cell(currentRow, 16).Value = "Frete x Tonelada";
                    worksheet.Cell(currentRow, 17).Value = "Frete Total";
                    worksheet.Cell(currentRow, 18).Value = "G3 x Tonelada";
                    worksheet.Cell(currentRow, 19).Value = "G3 Bruto";
                    worksheet.Cell(currentRow, 20).Value = "Pedagio";
                    worksheet.Cell(currentRow, 21).Value = "Despesas";
                    worksheet.Cell(currentRow, 22).Value = "Descontos";
                    worksheet.Cell(currentRow, 23).Value = "G3 Líquido";
                    worksheet.Cell(currentRow, 24).Value = "Margem";

                    foreach (var item in query)
                    {
                        currentRow++;

                        //ordem de servico
                        worksheet.Cell(currentRow, 1).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 1).Value = item.OrdemServico;

                        //numero de pedido
                        worksheet.Cell(currentRow, 2).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 2).Value = item.NumPedido;

                        //data de criacao
                        worksheet.Cell(currentRow, 3).DataType = XLDataType.DateTime;
                        worksheet.Cell(currentRow, 3).Value = item.DataCriacao.ToShortDateString();

                        //data da coleta
                        worksheet.Cell(currentRow, 4).DataType = XLDataType.DateTime;
                        worksheet.Cell(currentRow, 4).Value = item.DataColeta?.ToShortDateString();

                        //data da entrega
                        worksheet.Cell(currentRow, 5).DataType = XLDataType.DateTime;
                        worksheet.Cell(currentRow, 5).Value = item.DataEntrega?.ToShortDateString();

                        //cliente
                        worksheet.Cell(currentRow, 6).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 6).Value = item.Cliente?.RazaoSocial;

                        //caminhao
                        worksheet.Cell(currentRow, 7).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 7).Value = item.Caminhao?.Placa;

                        //motorista
                        worksheet.Cell(currentRow, 8).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 8).Value = item.Caminhao?.Motorista?.Nome;

                        //proprietario
                        worksheet.Cell(currentRow, 9).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 9).Value = item.Caminhao?.Proprietario?.Nome;

                        //remetente
                        worksheet.Cell(currentRow, 10).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 10).Value = item.Remetente?.RazaoSocial;

                        //local da coleta
                        worksheet.Cell(currentRow, 11).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 11).Value = item.LocalColeta;

                        //cte
                        worksheet.Cell(currentRow, 12).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 12).Value = item.CTe;

                        //nfe
                        worksheet.Cell(currentRow, 13).DataType = XLDataType.Text;
                        worksheet.Cell(currentRow, 13).Value = item.NFe;

                        //paletes
                        worksheet.Cell(currentRow, 14).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 14).Value = item.Quantidade;

                        //quantidade
                        worksheet.Cell(currentRow, 15).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 15).Value = item.Quantidade;

                        //frete x tonelada
                        worksheet.Cell(currentRow, 16).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 16).Value = item.FreteUnitario;

                        //frete total
                        worksheet.Cell(currentRow, 17).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 17).FormulaA1 = string.Format("{1}{0}*{2}{0}", currentRow, letters[13], letters[14]);

                        //g3 x tonelada
                        worksheet.Cell(currentRow, 18).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 18).Value = item.ComissaoUnitario;

                        //g3 bruto
                        worksheet.Cell(currentRow, 19).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 19).FormulaA1 = string.Format("{1}{0}*{2}{0}", currentRow, letters[13], letters[16]);

                        //pedagios
                        worksheet.Cell(currentRow, 20).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 20).Value = item.ValorPedagio;

                        //despesas
                        worksheet.Cell(currentRow, 21).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 21).Value = item.ValorAcrescimo;

                        //descontos
                        worksheet.Cell(currentRow, 22).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 22).Value = item.ValorDesconto;

                        //liquido g3
                        worksheet.Cell(currentRow, 23).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 23).FormulaA1 = string.Format("{1}{0}+{2}{0}+{3}{0}-{4}{0}", currentRow, letters[17], letters[18], letters[19], letters[20]);

                        //margem
                        worksheet.Cell(currentRow, 24).DataType = XLDataType.Number;
                        worksheet.Cell(currentRow, 24).FormulaA1 = string.Format("{1}{0}-{2}{0}", currentRow, letters[21], letters[15]);
                    }


                    using (var stream = new MemoryStream())
                    {
                        var filename = string.Format("g3_pedido_{0}{1}{2}_{3}{4}{5}.xlsx", DateTime.Now.Year,
                            DateTime.Now.Month.ToString().PadLeft(2, '0'),
                            DateTime.Now.Day.ToString().PadLeft(2, '0'),
                            DateTime.Now.Hour.ToString().PadLeft(2, '0'),
                            DateTime.Now.Minute.ToString().PadLeft(2, '0'),
                            DateTime.Now.Second.ToString().PadLeft(2, '0'));

                        var caminho = string.Format("{0}/exports/{1}", _enviroment.WebRootPath, filename);
                        workbook.SaveAs(caminho);

                        result.Item = "https://api-g3transportes.ecolinx.com.br/exports/" + filename;
                    }
                }

            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        private ValidationResult Valida_Estorno(Models.Pedido item)
        {
            var result = new ValidationResult();

            try
            {
                using var conn = new Contexts.EFContext();

                //verifica se foi coletado
                if (item.Coletado == false)
                {
                    result.IsValid = false;
                    result.Errors.Add("Pedido ainda nao foi coletado");
                }

                //verifica se tem lancamentos baixados
                var lanc = conn.LancamentoBaixa.Include(a => a.Lancamento).Where(a => a.Lancamento.IdPedido == item.Id).Any();
                if (lanc == true)
                {
                    result.IsValid = false;
                    result.Errors.Add("Já existem lançamentos baixados para esse pedido. Por favor, efetue o estorno das baixas antes de estornar a coleta");
                }
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
                result.Errors.Add(ex.InnerException.Message);
            }


            return result;
        }

        private void Trigger_ExcluiFinanceiro(Models.Pedido item)
        {
            try
            {
                using var conn = new Contexts.EFContext();

                var lancamentos = conn.Lancamento.Where(a => a.IdPedido == item.Id);

                foreach (var lanc in lancamentos)
                {
                    conn.Lancamento.Remove(lanc);
                }

                conn.SaveChanges();

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }

        private void Trigger_AtualizaFinanceiro(Models.Pedido item)
        {
            try
            {
                using var conn = new Contexts.EFContext();

                var contaReceber = conn.Lancamento.FirstOrDefault(a => a.IdPedido == item.Id && a.Tipo == "C");
                var contaPagar = conn.Lancamento.FirstOrDefault(a => a.IdPedido == item.Id && a.Tipo == "D");

                //atualiza liquido a receber
                if (contaReceber != null)
                {
                    if (contaReceber.Baixado == false)
                    {
                        if (contaReceber.ValorLiquido != item.ValorLiquido)
                        {
                            contaReceber.ValorBruto = item.ValorComissao;
                            contaReceber.ValorDesconto = item.ValorDesconto;
                            contaReceber.ValorAcrescimo = item.ValorAcrescimo + item.ValorPedagio;
                            contaReceber.ValorLiquido = item.ValorLiquido;
                            contaReceber.ValorSaldo = contaReceber.ValorLiquido - contaReceber.ValorBaixado;

                            conn.Lancamento.Update(contaReceber);
                            conn.SaveChanges();
                        }
                    }
                }


                //atualiza liquido a pagar
                if (contaPagar != null)
                {
                    if (contaPagar.Baixado == false)
                    {
                        if (contaPagar.ValorLiquido != item.ValorFrete)
                        {
                            contaPagar.ValorLiquido = item.ValorFrete;
                            contaPagar.ValorSaldo = contaPagar.ValorLiquido - contaPagar.ValorBaixado;

                            conn.Lancamento.Update(contaPagar);
                            conn.SaveChanges();
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }
    
        private void Trigger_InsereEstoque(Models.Pedido item)
        {
            try
            {
                if(item.IdRemetente != null)
                {
                    //verifica se tem paletes na transacao
                    if(item.Paletes > 0 && item.Coletado == true)
                    {
                        using(var conn = new Contexts.EFContext())
                        {
                            var cliente = conn.Cliente.FirstOrDefault(a => a.Id == item.IdCliente);

                            if(cliente != null)
                            {
                                var estoqueAtual = conn.RemetenteEstoque.FirstOrDefault(a => a.IdPedido == item.Id && a.Tipo == "D");

                                if(estoqueAtual == null)
                                {
                                    if(item.Coletado == true)
                                    {
                                        var estoque = new Models.RemetenteEstoque();
                                        estoque.IdPedido = item.Id;
                                        estoque.IdRemetente = item.IdRemetente.Value;
                                        estoque.Quantidade = item.Paletes;
                                        estoque.Tipo = "D";
                                        estoque.Descricao = string.Format("Coleta do pedido {0} - Cliente {1}", item.OrdemServico, cliente.RazaoSocial);
                                        estoque.Usuario = "";
                                        estoque.Transferencia = false;
                                        estoque.Data = item.DataColeta.Value;
                                        estoque.Ativo = true;

                                        conn.RemetenteEstoque.Add(estoque);
                                        conn.SaveChanges();
                                    }
                                    
                                }
                                else
                                {
                                    //atualiza quantidade
                                    estoqueAtual.Quantidade = item.Paletes;

                                    //salva estoque atual
                                    conn.RemetenteEstoque.Update(estoqueAtual);
                                    conn.SaveChanges();
                                }

                                //atualiza saldo
                                var repositoryEstoque = new RemetenteEstoque();
                                repositoryEstoque.Trigger_AtualizaSaldo(item.IdRemetente.Value);   
                                repositoryEstoque.Trigger_AtualizaPedido(item.Id);                             
                            }
                        }                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }

        private void Trigger_AtualizaEstoque(Models.Pedido item)
        {
            try
            {
                /*
                if(item.IdRemetente != null)
                {
                    using var conn = new Contexts.EFContext();

                    var estoque = conn.RemetenteEstoque.FirstOrDefault(a => a.IdPedido == item.Id &&);

                    if(estoque != null)
                    {
                        //atualiza a quantidade da movimentacao
                        estoque.Quantidade = item.Paletes;

                        //atualiza no banco de dados
                        conn.RemetenteEstoque.Update(estoque);
                        conn.SaveChanges();

                        //atualiza saldo
                        var repositoryEstoque = new RemetenteEstoque();
                        repositoryEstoque.Trigger_AtualizaSaldo(estoque.IdRemetente);
                    }
                }
                */
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro ao atualizar o saldo: {0}", ex.Message));
            }
        }

        private void Trigger_ExcluiEstoque(Models.Pedido item)
        {
            try
            {
                if(item.IdRemetente != null)
                {
                    using var conn = new Contexts.EFContext();

                    var estoque = conn.RemetenteEstoque.FirstOrDefault(a => a.IdPedido == item.Id);

                    if (estoque != null)
                    {
                        //atualiza no banco de dados
                        conn.RemetenteEstoque.Remove(estoque);
                        conn.SaveChanges();

                        //atualiza saldo
                        var repositoryEstoque = new RemetenteEstoque();
                        repositoryEstoque.Trigger_AtualizaSaldo(estoque.IdRemetente);
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
