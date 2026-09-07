using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class ContaBancariaRepository : BaseRepository<ContaBancaria>, IContaBancariaRepository
{
    public ContaBancariaRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<ContaBancaria>> ListarTodosAsync()
        => await _dbSet.Where(c => c.Ativa).ToListAsync();
}
