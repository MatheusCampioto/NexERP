using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class OrdemCompraRepository : BaseRepository<OrdemCompra>, IOrdemCompraRepository
{
    public OrdemCompraRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<OrdemCompra>> ListarTodosAsync()
        => await _dbSet
            .Include(o => o.Fornecedor)
            .Include(o => o.CondicaoPagamento)
            .Include(o => o.Itens).ThenInclude(i => i.Produto)
            .OrderByDescending(o => o.CriadoEm)
            .ToListAsync();

    public override async Task<OrdemCompra?> BuscarPorIdAsync(int id)
        => await _dbSet
            .Include(o => o.Fornecedor)
            .Include(o => o.CondicaoPagamento)
            .Include(o => o.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(o => o.Id == id);
}
