using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class SolicitacaoCompraRepository : BaseRepository<SolicitacaoCompra>, ISolicitacaoCompraRepository
{
    public SolicitacaoCompraRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<SolicitacaoCompra>> ListarTodosAsync()
        => await _dbSet
            .Include(s => s.Usuario)
            .Include(s => s.Itens).ThenInclude(i => i.Produto)
            .OrderByDescending(s => s.CriadoEm)
            .ToListAsync();

    public override async Task<SolicitacaoCompra?> BuscarPorIdAsync(int id)
        => await _dbSet
            .Include(s => s.Usuario)
            .Include(s => s.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(s => s.Id == id);
}
