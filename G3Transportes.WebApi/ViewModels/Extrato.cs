using System;
using System.Collections.Generic;
using System.Linq;
using G3Transportes.WebApi.Helpers;
using Microsoft.EntityFrameworkCore;

namespace G3Transportes.WebApi.ViewModels
{
    public class Extrato
    {
        public Extrato()
        {
            this.Creditos = new List<ExtratoLancamentos>();
            this.Debitos = new List<ExtratoLancamentos>();
        }

        public int Id { get; set; }
        public string Conta { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public double SaldoAnterior { get; set; }
        public double CreditoBaixado { get; set; }
        public double DebitoBaixado { get; set; }
        public double CreditoAberto { get; set; }
        public double DebitoAberto { get; set; }
        public double Saldo { get; set; }
        public double SaldoPrevisto { get; set; }

        public List<ExtratoLancamentos> Creditos { get; set; }
        public List<ExtratoLancamentos> Debitos { get; set; }
    }

    public class ExtratoFiltro
    {
        public ExtratoFiltro()
        {

        }

        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int? Conta { get; set; }
    }

    public class ExtratoLancamentos
    {
        public ExtratoLancamentos()
        {

        }

        public int Id { get; set; }
        public int? IdPedido { get; set; }
        public string Tipo { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? Coleta { get; set; }
        public DateTime? Entrega { get; set; }
        public DateTime? Vencimento { get; set; }
        public DateTime? Baixa { get; set; }
        public double ValorBaixado { get; set; }
        public string CentroCusto { get; set; }
        public string TipoDocumento { get; set; }
        public string ContaBancaria { get; set; }
        public string FormaPagamento { get; set; }
        public string OrdemServico { get; set; }
        public string NumPedido { get; set; }
        public string Cliente { get; set; }
        public string Proprietario { get; set; }
        public string Motorista { get; set; }
        public string Favorecido { get; set; }
        public string Remetente { get; set; }
        public string LocalColeta { get; set; }
        public string Cte { get; set; }
        public string Nfe { get; set; }
    }

    public class ExtratoRepository
    {
        public ExtratoRepository()
        {

        }

        public ListResult<Extrato> Pega(ExtratoFiltro filtro)
        {
            var result = new ListResult<Extrato>();

            try
            {
                //arruma data dos filtros
                if (filtro.DataInicio == null)
                    filtro.DataInicio = DateTime.Today.AddYears(-100);

                if (filtro.DataFim == null)
                    filtro.DataFim = DateTime.Today.AddYears(100);

                //abre a conexao com o banco de dados
                using var conn = new Contexts.EFContext();

                //filtra as contas
                var contas = filtro.Conta != null ? conn.ContaBancaria.Where(a => a.Id == filtro.Conta) : conn.ContaBancaria.AsQueryable();

                //faz loop nas contas
                foreach (var conta in contas)
                {
                    //cria extrato da conta
                    var extrato = new Extrato();
                    extrato.Id = conta.Id;
                    extrato.Conta = conta.Nome;
                    extrato.DataInicio = filtro.DataInicio;
                    extrato.DataFim = filtro.DataFim;
                    extrato.SaldoAnterior = PegaSaldoAnterior(filtro.DataInicio.Value, conta.Id);
                    extrato.CreditoBaixado = PegaValorBaixado(filtro, conta.Id, "C");
                    extrato.DebitoBaixado = PegaValorBaixado(filtro, conta.Id, "D");
                    extrato.CreditoAberto = PegaValorAberto(filtro, conta.Id, "C");
                    extrato.DebitoAberto = PegaValorAberto(filtro, conta.Id, "D");
                    extrato.Saldo = extrato.SaldoAnterior + (extrato.CreditoBaixado - extrato.DebitoBaixado);
                    extrato.SaldoPrevisto = extrato.Saldo + (extrato.CreditoAberto - extrato.DebitoAberto);
                    extrato.Creditos = PegaLancamentos(filtro, conta.Id, "C");
                    extrato.Debitos = PegaLancamentos(filtro, conta.Id, "D");

                    result.Items.Add(extrato);
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

        private double PegaSaldoAnterior(DateTime dataInicio, int idConta)
        {
            double result = 0;

            //abre a conexao com o banco de dados
            using var conn = new Contexts.EFContext();

            var conta = conn.ContaBancaria.FirstOrDefault(a => a.Id == idConta);
            var creditosAnteriores = conn.LancamentoBaixa.Where(a => a.Data.Date < dataInicio.Date && a.Tipo == "C" && a.IdContaBancaria == idConta).Sum(a => a.Valor);
            var debitosAnteriores = conn.LancamentoBaixa.Where(a => a.Data.Date < dataInicio.Date && a.Tipo == "D" && a.IdContaBancaria == idConta).Sum(a => a.Valor);

            //calcula saldo anterior
            result = conta.SaldoInicial + creditosAnteriores - debitosAnteriores;

            return result;
        }

        private double PegaValorAberto(ExtratoFiltro filtro, int idConta, string tipo)
        {
            double result = 0;

            //abre a conexao com o banco de dados
            using var conn = new Contexts.EFContext();

            result = conn.Lancamento
                         .Where(a => a.IdContaBancaria == idConta &&
                                     a.Tipo == tipo &&
                                     a.DataVencimento.Date >= filtro.DataInicio.Value.Date &&
                                     a.DataVencimento.Date <= filtro.DataFim.Value.Date &&
                                     a.Baixado == false)
                         .Sum(a => a.ValorSaldo);

            return result;
        }

        private double PegaValorBaixado(ExtratoFiltro filtro, int idConta, string tipo)
        {
            double result = 0;

            //abre a conexao com o banco de dados
            using var conn = new Contexts.EFContext();

            result = conn.LancamentoBaixa
                         .Where(a => a.IdContaBancaria == idConta &&
                                     a.Tipo == tipo &&
                                     a.Data.Date >= filtro.DataInicio.Value.Date &&
                                     a.Data.Date <= filtro.DataFim.Value.Date)
                         .Sum(a => a.Valor);

            return result;
        }

        private List<ExtratoLancamentos> PegaLancamentos(ExtratoFiltro filtro, int idConta, string tipo)
        {
            var result = new List<ExtratoLancamentos>();

            try
            {
                //abre a conexao com o banco de dados
                using var conn = new Contexts.EFContext();

                //faz a consulta
                var lancamentos = conn.LancamentoBaixa
                                      .Include(a => a.ContaBancaria)
                                      .Include(a => a.FormaPagamento)
                                      .Include(a => a.Lancamento).ThenInclude(b => b.Pedido)
                                      .Include(a => a.Lancamento).ThenInclude(b => b.Cliente)
                                      .Include(a => a.Lancamento).ThenInclude(b => b.Caminhao).ThenInclude(c => c.Motorista)
                                      .Include(a => a.Lancamento).ThenInclude(b => b.Caminhao).ThenInclude(c => c.Proprietario)
                                      .Include(a => a.Lancamento).ThenInclude(b => b.FormaPagamento)
                                      .Where(a => a.IdContaBancaria == idConta &&
                                                  a.Tipo == tipo &&
                                                  a.Data.Date >= filtro.DataInicio.Value.Date &&
                                                  a.Data.Date <= filtro.DataFim.Value.Date);

                //faz loop na lista
                foreach (var lanc in lancamentos)
                {

                    var item = new ExtratoLancamentos();
                    item.Id = lanc.IdLancamento;
                    item.IdPedido = lanc.Lancamento.IdPedido;
                    item.Tipo = lanc.Tipo;
                    item.Cliente = lanc.Lancamento.Cliente?.RazaoSocial;
                    item.ContaBancaria = lanc.ContaBancaria?.Nome;
                    item.Motorista = string.Format("{0} - {1}", lanc.Lancamento.Caminhao?.Placa, lanc.Lancamento.Caminhao?.Motorista?.Nome);
                    item.Favorecido = lanc.Lancamento.Favorecido;
                    item.FormaPagamento = lanc.FormaPagamento?.Nome;
                    item.CentroCusto = lanc.Lancamento.CentroCusto?.Nome;
                    item.TipoDocumento = lanc.Lancamento.TipoDocumento?.Nome;
                    item.Proprietario = lanc.Lancamento.Caminhao?.Proprietario?.Nome;
                    item.Remetente = lanc.Lancamento.Pedido?.Remetente?.RazaoSocial;
                    item.LocalColeta = lanc.Lancamento.Pedido?.LocalColeta;
                    item.OrdemServico = lanc.Lancamento.Pedido?.OrdemServico;
                    item.NumPedido = lanc.Lancamento.Pedido?.NumPedido;
                    item.Cte = lanc.Lancamento.Pedido?.CTe;
                    item.Nfe = lanc.Lancamento.Pedido?.NFe;
                    item.Coleta = lanc.Lancamento.Pedido?.DataColeta;
                    item.Entrega = lanc.Lancamento.Pedido?.DataEntrega;
                    item.Emissao = lanc.Lancamento.DataEmissao;
                    item.Vencimento = lanc.Lancamento.DataVencimento;
                    item.Baixa = lanc.Data.Date;
                    item.ValorBaixado = lanc.Valor;

                    result.Add(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            

            //retorna o resultado
            return result;
        }
    }
}
