using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;

namespace NexERP.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<LancamentoFinanceiro> LancamentosFinanceiros { get; set; }
    public DbSet<ContaBancaria> ContasBancarias { get; set; }
    public DbSet<OrdemServico> OrdensServico { get; set; }
    public DbSet<ItemOrdemServico> ItensOrdemServico { get; set; }
    public DbSet<CondicaoPagamento> CondicoesPagamento { get; set; }
    public DbSet<SolicitacaoCompra> SolicitacoesCompra { get; set; }
    public DbSet<ItemSolicitacaoCompra> ItensSolicitacaoCompra { get; set; }
    public DbSet<OrdemCompra> OrdensCompra { get; set; }
    public DbSet<ItemOrdemCompra> ItensOrdemCompra { get; set; }
    public DbSet<NotaFiscalEntrada> NotasFiscaisEntrada { get; set; }
    public DbSet<ItemNotaFiscalEntrada> ItensNotaFiscalEntrada { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(150);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.SenhaHash).IsRequired();
            entity.Property(e => e.Perfil).HasMaxLength(20);
            entity.Property(e => e.AcessoPessoas).HasDefaultValue(true);
            entity.Property(e => e.AcessoProdutos).HasDefaultValue(true);
            entity.Property(e => e.AcessoEstoque).HasDefaultValue(true);
            entity.Property(e => e.AcessoPedidos).HasDefaultValue(true);
            entity.Property(e => e.AcessoFinanceiro).HasDefaultValue(false);
            entity.Property(e => e.AcessoRelatorios).HasDefaultValue(false);
            entity.Property(e => e.AcessoUsuarios).HasDefaultValue(false);
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.TipoDocumento).HasMaxLength(10);
            entity.Property(e => e.Tipo).HasMaxLength(20);
            entity.Property(e => e.Funcao).HasMaxLength(100);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
            entity.Property(e => e.CPF).HasMaxLength(14);
            entity.Property(e => e.RG).HasMaxLength(20);
            entity.Property(e => e.EstadoCivil).HasMaxLength(20);
            entity.Property(e => e.Profissao).HasMaxLength(100);
            entity.Property(e => e.RazaoSocial).HasMaxLength(150);
            entity.Property(e => e.NomeFantasia).HasMaxLength(150);
            entity.Property(e => e.CNPJ).HasMaxLength(18);
            entity.Property(e => e.InscricaoEstadual).HasMaxLength(20);
            entity.Property(e => e.InscricaoMunicipal).HasMaxLength(20);
            entity.Property(e => e.NomeContato).HasMaxLength(150);
            entity.Property(e => e.Site).HasMaxLength(200);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Telefone).HasMaxLength(20);
            entity.Property(e => e.Celular).HasMaxLength(20);
            entity.Property(e => e.CEP).HasMaxLength(10);
            entity.Property(e => e.Endereco).HasMaxLength(200);
            entity.Property(e => e.Numero).HasMaxLength(10);
            entity.Property(e => e.Complemento).HasMaxLength(100);
            entity.Property(e => e.Bairro).HasMaxLength(100);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(2);
        });

        modelBuilder.Entity<Categoria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(200);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.CodigoBarras).HasMaxLength(50);
            entity.Property(e => e.Unidade).HasMaxLength(10);
            entity.Property(e => e.PrecoVenda).HasPrecision(18, 2);
            entity.Property(e => e.PrecoCusto).HasPrecision(18, 2);
            entity.HasOne(e => e.Categoria)
                  .WithMany()
                  .HasForeignKey(e => e.CategoriaId)
                  .IsRequired(false);
        });

        modelBuilder.Entity<MovimentacaoEstoque>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Tipo).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Quantidade).IsRequired();
            entity.Property(e => e.Observacao).HasMaxLength(200);
            entity.HasOne(e => e.Produto)
                  .WithMany()
                  .HasForeignKey(e => e.ProdutoId);
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.ValorTotal).HasPrecision(18, 2);
            entity.Property(e => e.Desconto).HasPrecision(18, 2);
            entity.Property(e => e.CondicaoPagamento).HasMaxLength(50);
            entity.Property(e => e.FormaPagamento).HasMaxLength(50);
            entity.Ignore(e => e.ValorLiquido);
            entity.HasOne(e => e.Pessoa)
                  .WithMany()
                  .HasForeignKey(e => e.PessoaId);
        });

        modelBuilder.Entity<ItemPedido>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.PrecoUnitario).HasPrecision(18, 2);
            entity.Property(e => e.Desconto).HasPrecision(18, 2);
            entity.Ignore(e => e.Subtotal);
            entity.HasOne(e => e.Pedido)
                  .WithMany(p => p.Itens)
                  .HasForeignKey(e => e.PedidoId);
            entity.HasOne(e => e.Produto)
                  .WithMany()
                  .HasForeignKey(e => e.ProdutoId);
        });

        modelBuilder.Entity<ContaBancaria>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Banco).HasMaxLength(100);
            entity.Property(e => e.Agencia).HasMaxLength(20);
            entity.Property(e => e.NumeroConta).HasMaxLength(20);
            entity.Property(e => e.SaldoInicial).HasPrecision(18, 2);
            entity.Property(e => e.SaldoAtual).HasPrecision(18, 2);
        });

        modelBuilder.Entity<LancamentoFinanceiro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Tipo).HasMaxLength(20);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Categoria).HasMaxLength(100);
            entity.Property(e => e.FormaPagamento).HasMaxLength(50);
            entity.Property(e => e.GrupoParcela).HasMaxLength(50);
            entity.HasOne(e => e.Pessoa)
                  .WithMany()
                  .HasForeignKey(e => e.PessoaId)
                  .IsRequired(false);
            entity.HasOne(e => e.Pedido)
                  .WithMany()
                  .HasForeignKey(e => e.PedidoId)
                  .IsRequired(false);
            entity.HasOne(e => e.ContaBancaria)
                  .WithMany()
                  .HasForeignKey(e => e.ContaBancariaId)
                  .IsRequired(false);
        });

        modelBuilder.Entity<OrdemServico>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Titulo).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Prioridade).HasMaxLength(20);
            entity.Property(e => e.Tecnico).HasMaxLength(100);
            entity.Property(e => e.ValorEstimado).HasPrecision(18, 2);
            entity.Property(e => e.ValorFinal).HasPrecision(18, 2);
            entity.HasOne(e => e.Pessoa)
                  .WithMany()
                  .HasForeignKey(e => e.PessoaId);
        });

        modelBuilder.Entity<ItemOrdemServico>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantidade).HasPrecision(18, 2);
            entity.Property(e => e.ValorUnitario).HasPrecision(18, 2);
            entity.Ignore(e => e.Subtotal);
            entity.HasOne(e => e.OrdemServico)
                  .WithMany(o => o.Itens)
                  .HasForeignKey(e => e.OrdemServicoId);
        });

        modelBuilder.Entity<CondicaoPagamento>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Descricao).HasMaxLength(200);
        });

        modelBuilder.Entity<SolicitacaoCompra>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.MotivoReprovacao).HasMaxLength(500);
            entity.HasOne(e => e.Usuario)
                  .WithMany()
                  .HasForeignKey(e => e.UsuarioId);
        });

        modelBuilder.Entity<ItemSolicitacaoCompra>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantidade).HasPrecision(18, 2);
            entity.Property(e => e.Unidade).HasMaxLength(10);
            entity.HasOne(e => e.SolicitacaoCompra)
                  .WithMany(s => s.Itens)
                  .HasForeignKey(e => e.SolicitacaoCompraId);
            entity.HasOne(e => e.Produto)
                  .WithMany()
                  .HasForeignKey(e => e.ProdutoId)
                  .IsRequired(false);
        });

        modelBuilder.Entity<OrdemCompra>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.ValorTotal).HasPrecision(18, 2);
            entity.HasOne(e => e.Fornecedor)
                  .WithMany()
                  .HasForeignKey(e => e.FornecedorId);
            entity.HasOne(e => e.SolicitacaoCompra)
                  .WithMany()
                  .HasForeignKey(e => e.SolicitacaoCompraId)
                  .IsRequired(false);
            entity.HasOne(e => e.CondicaoPagamento)
                  .WithMany()
                  .HasForeignKey(e => e.CondicaoPagamentoId)
                  .IsRequired(false);
        });

        modelBuilder.Entity<ItemOrdemCompra>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantidade).HasPrecision(18, 2);
            entity.Property(e => e.ValorUnitario).HasPrecision(18, 2);
            entity.Ignore(e => e.Subtotal);
            entity.HasOne(e => e.OrdemCompra)
                  .WithMany(o => o.Itens)
                  .HasForeignKey(e => e.OrdemCompraId);
            entity.HasOne(e => e.Produto)
                  .WithMany()
                  .HasForeignKey(e => e.ProdutoId)
                  .IsRequired(false);
        });

        modelBuilder.Entity<NotaFiscalEntrada>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.NumeroNF).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Serie).HasMaxLength(5);
            entity.Property(e => e.ChaveAcesso).HasMaxLength(44);
            entity.Property(e => e.ValorProdutos).HasPrecision(18, 2);
            entity.Property(e => e.ValorFrete).HasPrecision(18, 2);
            entity.Property(e => e.ValorImpostos).HasPrecision(18, 2);
            entity.Property(e => e.ValorTotal).HasPrecision(18, 2);
            entity.HasOne(e => e.OrdemCompra)
                  .WithMany()
                  .HasForeignKey(e => e.OrdemCompraId);
        });

        modelBuilder.Entity<ItemNotaFiscalEntrada>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Quantidade).HasPrecision(18, 2);
            entity.Property(e => e.ValorUnitario).HasPrecision(18, 2);
            entity.Ignore(e => e.ValorTotal);
            entity.HasOne(e => e.NotaFiscalEntrada)
                  .WithMany(n => n.Itens)
                  .HasForeignKey(e => e.NotaFiscalEntradaId);
            entity.HasOne(e => e.Produto)
                  .WithMany()
                  .HasForeignKey(e => e.ProdutoId)
                  .IsRequired(false);
        });
    }
}