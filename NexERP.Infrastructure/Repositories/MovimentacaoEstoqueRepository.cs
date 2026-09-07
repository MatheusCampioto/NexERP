using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class MovimentacaoEstoqueRepository : BaseRepository<MovimentacaoEstoque>, IMovimentacaoEstoqueRepository
{
    public MovimentacaoEstoqueRepository(AppDbContext context) : base(context) { }

    public async Task<IEnumerable<MovimentacaoEstoque>> ListarPorProdutoAsync(int produtoId)
        => await _dbSet
            .Include(m => m.Produto)
            .Where(m => m.ProdutoId == produtoId)
            .OrderByDescending(m => m.CriadoEm)
            .ToListAsync();
}
