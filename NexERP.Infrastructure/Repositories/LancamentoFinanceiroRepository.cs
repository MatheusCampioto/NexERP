using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class LancamentoFinanceiroRepository : BaseRepository<LancamentoFinanceiro>, ILancamentoFinanceiroRepository
{
    public LancamentoFinanceiroRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<LancamentoFinanceiro>> ListarTodosAsync()
        => await _dbSet
            .Include(l => l.Pessoa)
            .OrderByDescending(l => l.DataVencimento)
            .ToListAsync();

    public override async Task<LancamentoFinanceiro?> BuscarPorIdAsync(int id)
        => await _dbSet
            .Include(l => l.Pessoa)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarPorTipoAsync(string tipo)
        => await _dbSet
            .Include(l => l.Pessoa)
            .Where(l => l.Tipo == tipo)
            .OrderByDescending(l => l.DataVencimento)
            .ToListAsync();
}
