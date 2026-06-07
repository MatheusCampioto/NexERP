using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;

namespace NexERP.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<MovimentacaoEstoque> MovimentacoesEstoque { get; set; }
    public DbSet<Pedido> Pedidos { get; set; }
    public DbSet<ItemPedido> ItensPedido { get; set; }
    public DbSet<LancamentoFinanceiro> LancamentosFinanceiros { get; set; }

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
        });

        modelBuilder.Entity<Pessoa>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Tipo).HasMaxLength(20);
            entity.Property(e => e.CPF_CNPJ).HasMaxLength(20);
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.Telefone).HasMaxLength(20);
            entity.Property(e => e.Cidade).HasMaxLength(100);
            entity.Property(e => e.Estado).HasMaxLength(2);
            entity.Property(e => e.CEP).HasMaxLength(10);
        });

        modelBuilder.Entity<Produto>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Nome).IsRequired().HasMaxLength(150);
            entity.Property(e => e.Codigo).HasMaxLength(50);
            entity.Property(e => e.Unidade).HasMaxLength(10);
            entity.Property(e => e.Categoria).HasMaxLength(100);
            entity.Property(e => e.PrecoVenda).HasPrecision(18, 2);
            entity.Property(e => e.PrecoCusto).HasPrecision(18, 2);
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

        modelBuilder.Entity<LancamentoFinanceiro>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Tipo).HasMaxLength(20);
            entity.Property(e => e.Descricao).IsRequired().HasMaxLength(200);
            entity.Property(e => e.Valor).HasPrecision(18, 2);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Categoria).HasMaxLength(100);
            entity.HasOne(e => e.Pessoa)
                  .WithMany()
                  .HasForeignKey(e => e.PessoaId)
                  .IsRequired(false);
            entity.HasOne(e => e.Pedido)
                  .WithMany()
                  .HasForeignKey(e => e.PedidoId)
                  .IsRequired(false);
        });
    }
}