using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
{
    public PedidoRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<Pedido>> ListarTodosAsync()
        => await _dbSet
            .Include(p => p.Pessoa)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.CriadoEm)
            .ToListAsync();

    public override async Task<Pedido?> BuscarPorIdAsync(int id)
        => await _dbSet
            .Include(p => p.Pessoa)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);
}
