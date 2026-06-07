using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;

namespace NexERP.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Pessoa> Pessoas { get; set; }
    public DbSet<Produto> Produtos { get; set; }

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
    }
}