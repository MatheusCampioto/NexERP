using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class OrdemServicoRepository : BaseRepository<OrdemServico>, IOrdemServicoRepository
{
    public OrdemServicoRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<OrdemServico>> ListarTodosAsync()
        => await _dbSet
            .Include(o => o.Pessoa)
            .Include(o => o.Itens)
            .OrderByDescending(o => o.CriadoEm)
            .ToListAsync();

    public override async Task<OrdemServico?> BuscarPorIdAsync(int id)
        => await _dbSet
            .Include(o => o.Pessoa)
            .Include(o => o.Itens)
            .FirstOrDefaultAsync(o => o.Id == id);
}
