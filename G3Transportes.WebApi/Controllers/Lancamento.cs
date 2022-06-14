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
    [Route("lancamento")]
    public class Lancamento : Controller
    {
        private IQueryable<Models.Lancamento> Filtra(IQueryable<Models.Lancamento> query, Filters.Lancamento filtro)
        {
            //efetua verificacoes
            if (filtro.TipoFilter)
                query = query.Where(a => a.Tipo == filtro.TipoValue);

            if (filtro.CodigoFilter)
                query = query.Where(a => filtro.CodigoValue.Contains(a.Id));

            if (filtro.PedidoFilter)
                query = query.Where(a => a.IdPedido == filtro.PedidoValue);

            if (filtro.ClienteFilter)
                query = query.Where(a => a.IdCliente == filtro.ClienteValue);

            if (filtro.CaminhaoFilter)
                query = query.Where(a => a.IdCaminhao == filtro.CaminhaoValue);

            if (filtro.ProprietarioFilter)
                query = query.Where(a => a.Caminhao.IdProprietario == filtro.ProprietarioValue);

            if (filtro.FormaPagamentoFilter)
                query = query.Where(a => a.IdFormaPagamento == filtro.FormaPagamentoValue);

            if (filtro.ContaBancariaFilter)
                query = query.Where(a => a.IdContaBancaria == filtro.ContaBancariaValue);

            if (filtro.CentroCustoFilter)
                query = query.Where(a => a.IdCentroCusto == filtro.CentroCustoValue);

            if (filtro.TipoDocumentoFilter)
                query = query.Where(a => a.IdTipoDocumento == filtro.TipoDocumentoValue);

            if (filtro.FavorecidoFilter)
                query = query.Where(a => a.Favorecido.Contains(filtro.FavorecidoValue));

            if (filtro.EmissaoFilter)
            {
                if (filtro.EmissaoMinValue == null)
                    filtro.EmissaoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.EmissaoMaxValue == null)
                    filtro.EmissaoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataEmissao.Date >= filtro.EmissaoMinValue.Value.Date && a.DataEmissao.Date <= filtro.EmissaoMaxValue.Value.Date);
            }

            if (filtro.VencimentoFilter)
            {
                if (filtro.VencimentoMinValue == null)
                    filtro.VencimentoMinValue = DateTime.Today.AddYears(-100);
                if (filtro.VencimentoMaxValue == null)
                    filtro.VencimentoMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataVencimento.Date >= filtro.VencimentoMinValue.Value.Date && a.DataVencimento.Date <= filtro.VencimentoMaxValue.Value.Date);
            }

            if (filtro.BaixaFilter)
            {
                if (filtro.BaixaMinValue == null)
                    filtro.BaixaMinValue = DateTime.Today.AddYears(-100);
                if (filtro.BaixaMaxValue == null)
                    filtro.BaixaMaxValue = DateTime.Today.AddYears(100);

                query = query.Where(a => a.DataBaixa.Value.Date >= filtro.BaixaMinValue.Value.Date && a.DataBaixa.Value.Date <= filtro.BaixaMaxValue.Value.Date);
            }

            if (filtro.ValorLiquidoFilter)
                query = query.Where(a => a.ValorLiquido == filtro.ValorLiquidoValue);

            if (filtro.ValorBaixadoFilter)
                query = query.Where(a => a.ValorBaixado == filtro.ValorBaixadoValue);

            if (filtro.DescricaoFilter)
                query = query.Where(a => a.Observacao.Contains(filtro.DescricaoValue));

            if (filtro.BaixadoFilter)
                query = query.Where(a => a.Baixado == filtro.BaixadoValue);

            if (filtro.AutorizadoFilter)
                query = query.Where(a => a.Autorizado == filtro.AutorizadoValue);
            
            if (filtro.MesFilter)
            {
                query = query.Where(a => a.DataVencimento.Month == filtro.MesValue);
            }
            
            if (filtro.AnoFilter)
            {
                query = query.Where(a => a.DataVencimento.Year == filtro.AnoValue);
            }

            //retorna a query
            return query;
        }

        [HttpPost("lista/{pagina}/{tamanho}")]
        public ListResult<Models.Lancamento> Pega([FromBody] Filters.Lancamento filtro, int pagina, int tamanho)
        {
            var result = new ListResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Lancamento
                                .Include(a => a.Pedido)
                                .Include(a => a.Cliente)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Proprietario)
                                .Include(a => a.FormaPagamento)
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
                                    .Select(a => new Models.Lancamento
                                    {
                                        Id = a.Id,
                                        IdPedido = a.IdPedido,
                                        IdCliente = a.IdCliente,
                                        IdCaminhao = a.IdCaminhao,
                                        IdFormaPagamento = a.IdFormaPagamento,
                                        IdCentroCusto = a.IdCentroCusto,
                                        IdContaBancaria = a.IdContaBancaria,
                                        IdTipoDocumento = a.IdTipoDocumento,
                                        Tipo = a.Tipo,
                                        Favorecido = a.Favorecido,
                                        DataEmissao = a.DataEmissao,
                                        DataVencimento = a.DataVencimento,
                                        DataBaixa = a.DataBaixa,
                                        ValorBruto = a.ValorBruto,
                                        ValorDesconto = a.ValorDesconto,
                                        ValorAcrescimo = a.ValorAcrescimo,
                                        ValorLiquido = a.ValorLiquido,
                                        ValorBaixado = a.ValorBaixado,
                                        ValorSaldo = a.ValorSaldo,
                                        Observacao = a.Observacao,
                                        Baixado = a.Baixado,
                                        BancoNome = a.BancoNome,
                                        BancoAgencia = a.BancoAgencia,
                                        BancoOperacao = a.BancoOperacao,
                                        BancoConta = a.BancoConta,
                                        BancoTitular = a.BancoTitular,
                                        BancoDocumento = a.BancoDocumento,
                                        Autorizado = a.Autorizado,
                                        DataAutorizacao = a.DataAutorizacao,
                                        UsuarioAutorizacao = a.UsuarioAutorizacao,
                                        Pedido = a.Pedido != null ? new Models.Pedido
                                        {
                                            Id = a.Pedido.Id,
                                            OrdemServico = a.Pedido.OrdemServico,
                                            NumPedido = a.Pedido.NumPedido,
                                            CTe = a.Pedido.CTe,
                                            NFe = a.Pedido.NFe,
                                            Boleto = a.Pedido.Boleto
                                        } : null,
                                        Cliente = a.Cliente != null ? new Models.Cliente
                                        {
                                            Id = a.Cliente.Id,
                                            RazaoSocial = a.Cliente.RazaoSocial,
                                            NomeFantasia = a.Cliente.NomeFantasia,
                                            Documento1 = a.Cliente.Documento1,
                                            Telefone1 = a.Cliente.Telefone1
                                        } : null,
                                        Caminhao = a.Caminhao != null ? new Models.Caminhao
                                        {
                                            Id = a.Caminhao.Id,
                                            Placa = a.Caminhao.Placa,
                                            Motorista = new Models.Motorista
                                            {
                                                Id = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Id : 0,
                                                Nome = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Nome : "Não Informado",
                                                Documento1 = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Documento1 : "",
                                                Telefone1 = a.Caminhao.Motorista != null ? a.Caminhao.Motorista.Telefone1 : "",
                                            },
                                            Proprietario = new Models.Proprietario
                                            {
                                                Id = a.Caminhao.Proprietario != null ? a.Caminhao.Proprietario.Id : 0,
                                                Nome = a.Caminhao.Proprietario != null ? a.Caminhao.Proprietario.Nome : "Não Informado"
                                            }
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
                                        } : null,
                                        CentroCusto = a.CentroCusto != null ? new Models.CentroCusto
                                        {
                                            Id = a.CentroCusto.Id,
                                            Nome = a.CentroCusto.Nome
                                        } : null,
                                        TipoDocumento = a.TipoDocumento != null ? new Models.TipoDocumento
                                        {
                                            Id = a.TipoDocumento.Id,
                                            Nome = a.TipoDocumento.Nome
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
        public ItemResult<Models.Lancamento> Pega(int id)
        {
            var result = new ItemResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Lancamento
                                .Include(a => a.Pedido)
                                .Include(a => a.Cliente)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Proprietario)
                                .Include(a => a.FormaPagamento)
                                .Select(a => new Models.Lancamento
                                {
                                    Id = a.Id,
                                    IdPedido = a.IdPedido,
                                    IdCliente = a.IdCliente,
                                    IdCaminhao = a.IdCaminhao,
                                    IdFormaPagamento = a.IdFormaPagamento,
                                    IdCentroCusto = a.IdCentroCusto,
                                    IdContaBancaria = a.IdContaBancaria,
                                    IdTipoDocumento = a.IdTipoDocumento,
                                    Tipo = a.Tipo,
                                    Favorecido = a.Favorecido,
                                    DataEmissao = a.DataEmissao,
                                    DataVencimento = a.DataVencimento,
                                    DataBaixa = a.DataBaixa,
                                    ValorBruto = a.ValorBruto,
                                    ValorDesconto = a.ValorDesconto,
                                    ValorAcrescimo = a.ValorAcrescimo,
                                    ValorLiquido = a.ValorLiquido,
                                    ValorBaixado = a.ValorBaixado,
                                    ValorSaldo = a.ValorSaldo,
                                    Observacao = a.Observacao,
                                    Baixado = a.Baixado,
                                    BancoNome = a.BancoNome,
                                    BancoAgencia = a.BancoAgencia,
                                    BancoOperacao = a.BancoOperacao,
                                    BancoConta = a.BancoConta,
                                    BancoTitular = a.BancoTitular,
                                    BancoDocumento = a.BancoDocumento,
                                    Autorizado = a.Autorizado,
                                    DataAutorizacao = a.DataAutorizacao,
                                    UsuarioAutorizacao = a.UsuarioAutorizacao,
                                    Pedido = a.Pedido != null ? new Models.Pedido
                                    {
                                        Id = a.Pedido.Id,
                                        OrdemServico = a.Pedido.OrdemServico,
                                        NumPedido = a.Pedido.NumPedido,
                                        CTe = a.Pedido.CTe,
                                        NFe = a.Pedido.NFe,
                                        Boleto = a.Pedido.Boleto
                                    } : null,
                                    Cliente = a.Cliente != null ? new Models.Cliente
                                    {
                                        Id = a.Cliente.Id,
                                        RazaoSocial = a.Cliente.RazaoSocial,
                                        NomeFantasia = a.Cliente.NomeFantasia,
                                        Documento1 = a.Cliente.Documento1,
                                        Telefone1 = a.Cliente.Telefone1
                                    } : null,
                                    Caminhao = a.Caminhao != null ? new Models.Caminhao
                                    {
                                        Id = a.Caminhao.Id,
                                        Placa = a.Caminhao.Placa,
                                        Motorista = a.Caminhao.Motorista != null ? new Models.Motorista
                                        {
                                            Id = a.Caminhao.Motorista.Id,
                                            Nome = a.Caminhao.Motorista.Nome,
                                            Documento1 = a.Caminhao.Motorista.Documento1,
                                            Telefone1 = a.Caminhao.Motorista.Telefone1,
                                        } : null,
                                        Proprietario = a.Caminhao.Proprietario != null ? new Models.Proprietario
                                        {
                                            Id = a.Caminhao.Proprietario.Id,
                                            Nome = a.Caminhao.Proprietario.Nome
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
                                    } : null,
                                    CentroCusto = a.CentroCusto != null ? new Models.CentroCusto
                                    {
                                        Id = a.CentroCusto.Id,
                                        Nome = a.CentroCusto.Nome
                                    } : null,
                                    TipoDocumento = a.TipoDocumento != null ? new Models.TipoDocumento
                                    {
                                        Id = a.TipoDocumento.Id,
                                        Nome = a.TipoDocumento.Nome
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

        [HttpPost("resumo/cliente")]
        public ListResult<ViewModels.LancamentoResumo> ResumoCliente([FromBody] Filters.Lancamento filtro)
        {
            var result = new ListResult<ViewModels.LancamentoResumo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Lancamento
                                .Include(a => a.Cliente)
                                .Include(a => a.Pedido).ThenInclude(b => b.Remetente)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Proprietario)
                                .Include(a => a.FormaPagamento)
                                .Include(a => a.CentroCusto)
                                .Include(a => a.ContaBancaria)
                                .Include(a => a.TipoDocumento)
                                .AsQueryable();

                //efetua o filtro
                query = query.Where(a => a.Cliente != null);
                query = Filtra(query, filtro);

                //configura registros
                var grupo = query.ToList().GroupBy(a => a.Cliente);

                foreach (var item in grupo)
                {
                    //inicializa item
                    var resumo = new ViewModels.LancamentoResumo();
                    resumo.Id = item.Key.Id;
                    resumo.Nome = item.Key.RazaoSocial;
                    resumo.EmissaoInicio = filtro.EmissaoMinValue;
                    resumo.EmissaoFim = filtro.EmissaoMaxValue;
                    resumo.VencimentoInicio = filtro.VencimentoMinValue;
                    resumo.VencimentoFim = filtro.VencimentoMaxValue;
                    resumo.TotalPagar = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "D" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalPago = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "D" && a.Baixado == true).Sum(b => b.ValorBaixado);
                    resumo.TotalReceber = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "C" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalRecebido = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "C" && a.Baixado == true).Sum(b => b.ValorBaixado);

                    //carrega items a pagar
                    var listaPagar = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "D" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPagar)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }

                        
                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPagar.Add(resumoItem);
                    }

                    //carrega items pagos
                    var listaPago = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "D" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPago)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPago.Add(resumoItem);
                    }

                    //carrega items a receber
                    var listaReceber = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "C" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaReceber)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsReceber.Add(resumoItem);
                    }

                    //carrega items recebidos
                    var listaRecebido = query.Where(a => a.IdCliente == item.Key.Id && a.Tipo == "C" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaRecebido)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsRecebido.Add(resumoItem);
                    }

                    //adiciona na lista
                    result.Items.Add(resumo);
                }

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = 1;
                result.PageSize = 10000;
                result.TotalItems = query.Count();
                result.TotalPages = 1;
                
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("resumo/caminhao")]
        public ListResult<ViewModels.LancamentoResumo> ResumoCaminhao([FromBody] Filters.Lancamento filtro)
        {
            var result = new ListResult<ViewModels.LancamentoResumo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Lancamento
                                .Include(a => a.Cliente)
                                .Include(a => a.Pedido).ThenInclude(b => b.Remetente)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.FormaPagamento)
                                .Include(a => a.CentroCusto)
                                .Include(a => a.ContaBancaria)
                                .Include(a => a.TipoDocumento)
                                .AsQueryable();

                //efetua o filtro
                query = Filtra(query, filtro);

                //configura registros
                var grupo = query.ToList().GroupBy(a => a.Caminhao);

                foreach (var item in grupo)
                {
                    var nome = "Nenhum";
                    if (item.Key != null)
                    {
                        if (item.Key.Motorista != null)
                        {
                            nome = string.Format("{0} - {1}", item.Key.Placa, item.Key.Motorista.Nome);
                        }
                    }

                    //inicializa item
                    var resumo = new ViewModels.LancamentoResumo();
                    resumo.Id = item.Key.Id;
                    resumo.Nome = nome;
                    resumo.EmissaoInicio = filtro.EmissaoMinValue;
                    resumo.EmissaoFim = filtro.EmissaoMaxValue;
                    resumo.VencimentoInicio = filtro.VencimentoMinValue;
                    resumo.VencimentoFim = filtro.VencimentoMaxValue;
                    resumo.TotalPagar = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "D" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalPago = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "D" && a.Baixado == true).Sum(b => b.ValorBaixado);
                    resumo.TotalReceber = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "C" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalRecebido = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "C" && a.Baixado == true).Sum(b => b.ValorBaixado);

                    //carrega items a pagar
                    var listaPagar = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "D" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPagar)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPagar.Add(resumoItem);
                    }

                    //carrega items pagos
                    var listaPago = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "D" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPago)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPago.Add(resumoItem);
                    }

                    //carrega items a receber
                    var listaReceber = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "C" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaReceber)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsReceber.Add(resumoItem);
                    }

                    //carrega items recebidos
                    var listaRecebido = query.Where(a => a.IdCaminhao == item.Key.Id && a.Tipo == "C" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaRecebido)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsRecebido.Add(resumoItem);
                    }

                    //adiciona na lista
                    result.Items.Add(resumo);
                }

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = 1;
                result.PageSize = 10000;
                result.TotalItems = query.Count();
                result.TotalPages = 1;

            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("resumo/proprietario")]
        public ListResult<ViewModels.LancamentoResumo> ResumoProprietario([FromBody] Filters.Lancamento filtro)
        {
            var result = new ListResult<ViewModels.LancamentoResumo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a consulta
                var query = conn.Lancamento
                                .Include(a => a.Cliente)
                                .Include(a => a.Pedido).ThenInclude(b => b.Remetente)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Proprietario)
                                .Include(a => a.FormaPagamento)
                                .Include(a => a.CentroCusto)
                                .Include(a => a.ContaBancaria)
                                .Include(a => a.TipoDocumento)
                                .AsQueryable();

                //efetua o filtro
                query = query.Where(a => a.Caminhao.IdProprietario != null);
                query = Filtra(query, filtro);

                //configura registros
                var grupo = query.ToList().GroupBy(a => a.Caminhao.Proprietario);

                foreach (var item in grupo)
                {
                    //inicializa item
                    var resumo = new ViewModels.LancamentoResumo();
                    resumo.Id = item.Key.Id;
                    resumo.Nome = item.Key.Nome;
                    resumo.EmissaoInicio = filtro.EmissaoMinValue;
                    resumo.EmissaoFim = filtro.EmissaoMaxValue;
                    resumo.VencimentoInicio = filtro.VencimentoMinValue;
                    resumo.VencimentoFim = filtro.VencimentoMaxValue;
                    resumo.TotalPagar = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "D" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalPago = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "D" && a.Baixado == true).Sum(b => b.ValorBaixado);
                    resumo.TotalReceber = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "C" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalRecebido = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "C" && a.Baixado == true).Sum(b => b.ValorBaixado);

                    //carrega items a pagar
                    var listaPagar = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "D" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPagar)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPagar.Add(resumoItem);
                    }

                    //carrega items pagos
                    var listaPago = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "D" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPago)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPago.Add(resumoItem);
                    }

                    //carrega items a receber
                    var listaReceber = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "C" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaReceber)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsReceber.Add(resumoItem);
                    }

                    //carrega items recebidos
                    var listaRecebido = query.Where(a => a.Caminhao.IdProprietario == item.Key.Id && a.Tipo == "C" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaRecebido)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsRecebido.Add(resumoItem);
                    }

                    //adiciona na lista
                    result.Items.Add(resumo);
                }

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = 1;
                result.PageSize = 10000;
                result.TotalItems = query.Count();
                result.TotalPages = 1;

            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("resumo/centro-custo")]
        public ListResult<ViewModels.LancamentoResumo> ResumoCentroCusto([FromBody] Filters.Lancamento filtro)
        {
            var result = new ListResult<ViewModels.LancamentoResumo>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a consulta
                var query = conn.Lancamento
                                .Include(a => a.Cliente)
                                .Include(a => a.Pedido).ThenInclude(b => b.Remetente)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Motorista)
                                .Include(a => a.Caminhao).ThenInclude(b => b.Proprietario)
                                .Include(a => a.FormaPagamento)
                                .Include(a => a.CentroCusto)
                                .Include(a => a.ContaBancaria)
                                .Include(a => a.TipoDocumento)
                                .AsQueryable();

                //efetua o filtro
                query = query.Where(a => a.CentroCusto != null);
                query = Filtra(query, filtro);

                //configura registros
                var grupo = query.ToList().GroupBy(a => a.CentroCusto);

                foreach (var item in grupo)
                {
                    //inicializa item
                    var resumo = new ViewModels.LancamentoResumo();
                    resumo.Id = item.Key.Id;
                    resumo.Nome = item.Key.Nome;
                    resumo.Referencia = item.Key.Referencia;
                    resumo.EmissaoInicio = filtro.EmissaoMinValue;
                    resumo.EmissaoFim = filtro.EmissaoMaxValue;
                    resumo.VencimentoInicio = filtro.VencimentoMinValue;
                    resumo.VencimentoFim = filtro.VencimentoMaxValue;
                    resumo.TotalPagar = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "D" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalPago = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "D" && a.Baixado == true).Sum(b => b.ValorBaixado);
                    resumo.TotalReceber = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "C" && a.Baixado == false).Sum(b => b.ValorSaldo);
                    resumo.TotalRecebido = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "C" && a.Baixado == true).Sum(b => b.ValorBaixado);

                    //carrega items a pagar
                    var listaPagar = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "D" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPagar)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPagar.Add(resumoItem);
                    }

                    //carrega items pagos
                    var listaPago = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "D" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaPago)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsPago.Add(resumoItem);
                    }

                    //carrega items a receber
                    var listaReceber = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "C" && a.Baixado == false).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaReceber)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsReceber.Add(resumoItem);
                    }

                    //carrega items recebidos
                    var listaRecebido = query.Where(a => a.IdCentroCusto == item.Key.Id && a.Tipo == "C" && a.Baixado == true).OrderBy(a => a.IdPedido);
                    foreach (var subItem in listaRecebido)
                    {
                        //inicializa os items
                        var cliente = subItem.Cliente != null ? subItem.Cliente.RazaoSocial : "Nao Informado";
                        var motorista = "";
                        var proprietario = "";
                        var remetente = "Nenhum";

                        if (subItem.Caminhao != null)
                        {
                            motorista = subItem.Caminhao.Motorista != null ? string.Format("{0} - {1}", subItem.Caminhao.Placa, subItem.Caminhao.Motorista.Nome) : "Nao Informado";
                            proprietario = subItem.Caminhao.Proprietario != null ? subItem.Caminhao.Proprietario.Nome : "Nao Informado";
                        }


                        if (subItem.Pedido != null)
                        {
                            remetente = subItem.Pedido.Remetente != null ? subItem.Pedido.Remetente.RazaoSocial : "Nao Informado";
                        }

                        var resumoItem = new ViewModels.LancamentoResumoItem();
                        resumoItem.Id = subItem.Id;
                        resumoItem.IdPedido = subItem.IdPedido;
                        resumoItem.Tipo = subItem.Tipo;
                        resumoItem.Emissao = subItem.DataEmissao;
                        resumoItem.Coleta = subItem.Pedido?.DataColeta;
                        resumoItem.Entrega = subItem.Pedido?.DataEntrega;
                        resumoItem.Vencimento = subItem.DataVencimento;
                        resumoItem.Baixa = subItem.DataBaixa;
                        resumoItem.Valor = subItem.ValorLiquido;
                        resumoItem.ValorBaixado = subItem.ValorBaixado;
                        resumoItem.ValorSaldo = subItem.ValorSaldo;
                        resumoItem.CentroCusto = subItem.CentroCusto != null ? subItem.CentroCusto.Nome : "Nenhum";
                        resumoItem.TipoDocumento = subItem.TipoDocumento != null ? subItem.TipoDocumento.Nome : "Nenhum";
                        resumoItem.ContaBancaria = subItem.ContaBancaria != null ? subItem.ContaBancaria.Nome : "Nenhum";
                        resumoItem.FormaPagamento = subItem.FormaPagamento != null ? subItem.FormaPagamento.Nome : "Nenhum";
                        resumoItem.OrdemServico = subItem.Pedido != null ? subItem.Pedido.OrdemServico : "";
                        resumoItem.NumPedido = subItem.Pedido != null ? subItem.Pedido.NumPedido : "";
                        resumoItem.Cliente = cliente;
                        resumoItem.Proprietario = proprietario;
                        resumoItem.Motorista = motorista;
                        resumoItem.Remetente = remetente;
                        resumoItem.Favorecido = subItem.Favorecido;
                        resumoItem.LocalColeta = subItem.Pedido != null ? subItem.Pedido.LocalColeta : "";
                        resumoItem.Cte = subItem.Pedido != null ? subItem.Pedido.CTe : "";
                        resumoItem.Nfe = subItem.Pedido != null ? subItem.Pedido.NFe : "";

                        //adiciona na lista
                        resumo.ItemsRecebido.Add(resumoItem);
                    }

                    //adiciona na lista
                    result.Items.Add(resumo);
                }

                //configura o resultado
                result.IsValid = true;
                result.CurrentPage = 1;
                result.PageSize = 10000;
                result.TotalItems = query.Count();
                result.TotalPages = 1;

            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.Errors.Add(ex.Message);
            }

            return result;
        }

        [HttpPost("insere")]
        public ItemResult<Models.Lancamento> Insere([FromBody]Models.Lancamento item)
        {
            var result = new ItemResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa as referencias
                item.Pedido = null;
                item.Cliente = null;
                item.Caminhao = null;
                item.FormaPagamento = null;
                item.ContaBancaria = null;
                item.CentroCusto = null;
                item.TipoDocumento = null;
                item.Baixas = null;
                item.Anexos = null;

                //inicializa saldo
                item.ValorSaldo = item.ValorLiquido - item.ValorBaixado;

                //inicializa dados bancarios
                item = InicializaDadosBancarios(item);

                //inicializa a query
                conn.Lancamento.Add(item);
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
        public ItemResult<Models.Lancamento> Atualiza([FromBody]Models.Lancamento item)
        {
            var result = new ItemResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa as referencias
                item.Pedido = null;
                item.Cliente = null;
                item.Caminhao = null;
                item.FormaPagamento = null;
                item.ContaBancaria = null;
                item.CentroCusto = null;
                item.TipoDocumento = null;
                item.Baixas = null;
                item.Anexos = null;

                //inicializa saldo
                item.ValorSaldo = item.ValorLiquido - item.ValorBaixado;

                //inicializa a query
                conn.Lancamento.Update(item);
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

        [HttpPut("baixa")]
        public ItemResult<Models.Lancamento> Baixa([FromBody]Models.Lancamento item)
        {
            var result = new ItemResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa as referencias
                item.Pedido = null;
                item.Cliente = null;
                item.Caminhao = null;
                item.FormaPagamento = null;
                item.ContaBancaria = null;
                item.CentroCusto = null;
                item.TipoDocumento = null;
                item.Baixas = null;
                item.Anexos = null;

                //atualiza items
                item.Baixado = true;

                if (item.DataBaixa == null)
                {
                    item.DataBaixa = DateTime.Today;
                }
                
                //inicializa a query
                conn.Lancamento.Update(item);
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
        public ItemResult<Models.Lancamento> Estorna([FromBody]Models.Lancamento item)
        {
            var result = new ItemResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //limpa as referencias
                item.Pedido = null;
                item.Cliente = null;
                item.Caminhao = null;
                item.FormaPagamento = null;
                item.ContaBancaria = null;
                item.CentroCusto = null;
                item.TipoDocumento = null;
                item.Baixas = null;
                item.Anexos = null;

                //atualiza items
                item.Baixado = false;
                item.DataBaixa = null;

                //inicializa a query
                conn.Lancamento.Update(item);
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
        public ItemResult<Models.Lancamento> Deleta(int id)
        {
            var result = new ItemResult<Models.Lancamento>();

            try
            {
                using var conn = new Contexts.EFContext();

                //inicializa a query
                var query = conn.Lancamento.FirstOrDefault(a => a.Id == id);

                if (query != null)
                {
                    conn.Lancamento.Remove(query);
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

        private Models.Lancamento InicializaDadosBancarios(Models.Lancamento item)
        {
            try
            {
                using var conn = new Contexts.EFContext();

                if (item.Tipo == "D")
                {
                    if (item.IdCaminhao != null)
                    {
                        var caminhao = conn.Caminhao
                                           .Include(a => a.Motorista)
                                           .FirstOrDefault(a => a.Id == item.IdCaminhao);

                        if (caminhao.Motorista != null)
                        {
                            item.BancoNome = caminhao.Motorista.BancoNome;
                            item.BancoAgencia = caminhao.Motorista.BancoAgencia;
                            item.BancoOperacao = caminhao.Motorista.BancoOperacao;
                            item.BancoConta = caminhao.Motorista.BancoConta;
                            item.BancoTitular = caminhao.Motorista.BancoTitular;
                            item.BancoDocumento = caminhao.Motorista.BancoDocumento;
                        }
                    }
                    else
                    {
                        if (item.IdCliente != null)
                        {
                            var cliente = conn.Cliente.FirstOrDefault(a => a.Id == item.IdCliente);

                            item.BancoNome = cliente.BancoNome;
                            item.BancoAgencia = cliente.BancoAgencia;
                            item.BancoOperacao = cliente.BancoOperacao;
                            item.BancoConta = cliente.BancoConta;
                            item.BancoTitular = cliente.BancoTitular;
                            item.BancoDocumento = cliente.BancoDocumento;
                        }
                    }
                }
            }
            catch (Exception)
            {
                item.BancoNome = "";
                item.BancoAgencia = "";
                item.BancoOperacao = "";
                item.BancoConta = "";
                item.BancoTitular = "";
                item.BancoDocumento = "";
            }
            

            return item;
        }
    }
}
