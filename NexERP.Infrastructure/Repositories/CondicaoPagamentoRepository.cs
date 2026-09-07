using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class CondicaoPagamentoRepository : BaseRepository<CondicaoPagamento>, ICondicaoPagamentoRepository
{
    public CondicaoPagamentoRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<CondicaoPagamento>> ListarTodosAsync()
        => await _dbSet.Where(c => c.Ativa).ToListAsync();
}
