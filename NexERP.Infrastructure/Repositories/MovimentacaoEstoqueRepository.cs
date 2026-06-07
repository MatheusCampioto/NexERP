using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class MovimentacaoEstoqueRepository : IMovimentacaoEstoqueRepository
{
    private readonly AppDbContext _context;

    public MovimentacaoEstoqueRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovimentacaoEstoque>> ListarPorProdutoAsync(int produtoId)
        => await _context.MovimentacoesEstoque
            .Include(m => m.Produto)
            .Where(m => m.ProdutoId == produtoId)
            .OrderByDescending(m => m.CriadoEm)
            .ToListAsync();

    public async Task AdicionarAsync(MovimentacaoEstoque movimentacao)
        => await _context.MovimentacoesEstoque.AddAsync(movimentacao);

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}