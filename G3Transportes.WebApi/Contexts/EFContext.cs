using System;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace G3Transportes.WebApi.Contexts
{
    public class EFContext : DbContext
    {
        public EFContext() : base()
        {
            
        }

        #region PROPRIEDADES

        public DbSet<Models.Anexo> Anexo { get; set; }
        public DbSet<Models.Caminhao> Caminhao { get; set; }
        public DbSet<Models.CaminhaoAnexo> CaminhaoAnexo { get; set; }
        public DbSet<Models.CentroCusto> CentroCusto { get; set; }
        public DbSet<Models.Cliente> Cliente { get; set; }
        public DbSet<Models.ClienteAnexo> ClienteAnexo { get; set; }
        public DbSet<Models.Conciliacao> Conciliacao { get; set; }
        public DbSet<Models.Configuracao> Configuracao { get; set; }
        public DbSet<Models.ContaBancaria> ContaBancaria { get; set; }
        public DbSet<Models.FormaPagamento> FormaPagamento { get; set; }
        public DbSet<Models.Lancamento> Lancamento { get; set; }
        public DbSet<Models.LancamentoAnexo> LancamentoAnexo { get; set; }
        public DbSet<Models.LancamentoBaixa> LancamentoBaixa { get; set; }
        public DbSet<Models.Motorista> Motorista { get; set; }
        public DbSet<Models.MotoristaAnexo> MotoristaAnexo { get; set; }
        public DbSet<Models.Pedido> Pedido { get; set; }
        public DbSet<Models.PedidoAnexo> PedidoAnexo { get; set; }
        public DbSet<Models.Proprietario> Proprietario { get; set; }
        public DbSet<Models.ProprietarioAnexo> ProprietarioAnexo { get; set; }
        public DbSet<Models.Remetente> Remetente { get; set; }
        public DbSet<Models.RemetenteAnexo> RemetenteAnexo { get; set; }
        public DbSet<Models.RemetenteEstoque> RemetenteEstoque { get; set; }
        public DbSet<Models.TipoDocumento> TipoDocumento { get; set; }
        public DbSet<Models.Usuario> Usuario { get; set; }

        #endregion

        #region EVENTOS

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
            optionsBuilder.UseMySql("Server=vps20972.publiccloud.com.br;Port=3306;Database=g3transportes;User=ecolinx;Password=c@dmin2014;", options =>
            {
                options.ServerVersion(new Version(5, 7, 27), ServerType.MySql);
            });

            /*
            optionsBuilder.UseMySql("Server=vps20972.publiccloud.com.br;Port=3306;Database=g3financ;User=ecolinx;Password=c@dmin2014;", options =>
            {
                options.ServerVersion(new Version(5, 7, 27), ServerType.MySql);
            });
            */
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Mappings.Anexo.Map(modelBuilder);
            Mappings.Caminhao.Map(modelBuilder);
            Mappings.CaminhaoAnexo.Map(modelBuilder);
            Mappings.CentroCusto.Map(modelBuilder);
            Mappings.Cliente.Map(modelBuilder);
            Mappings.ClienteAnexo.Map(modelBuilder);
            Mappings.Conciliacao.Map(modelBuilder);
            Mappings.Configuracao.Map(modelBuilder);
            Mappings.ContaBancaria.Map(modelBuilder);
            Mappings.FormaPagamento.Map(modelBuilder);
            Mappings.Lancamento.Map(modelBuilder);
            Mappings.LancamentoAnexo.Map(modelBuilder);
            Mappings.LancamentoBaixa.Map(modelBuilder);
            Mappings.Motorista.Map(modelBuilder);
            Mappings.MotoristaAnexo.Map(modelBuilder);
            Mappings.Pedido.Map(modelBuilder);
            Mappings.PedidoAnexo.Map(modelBuilder);
            Mappings.Proprietario.Map(modelBuilder);
            Mappings.ProprietarioAnexo.Map(modelBuilder);
            Mappings.Remetente.Map(modelBuilder);
            Mappings.RemetenteAnexo.Map(modelBuilder);
            Mappings.RemetenteEstoque.Map(modelBuilder);
            Mappings.TipoDocumento.Map(modelBuilder);
            Mappings.Usuario.Map(modelBuilder);
        }

        #endregion
    }
}
