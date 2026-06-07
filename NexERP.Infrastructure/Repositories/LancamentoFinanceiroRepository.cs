using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class LancamentoFinanceiroRepository : ILancamentoFinanceiroRepository
{
    private readonly AppDbContext _context;

    public LancamentoFinanceiroRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarTodosAsync()
        => await _context.LancamentosFinanceiros
            .Include(l => l.Pessoa)
            .OrderByDescending(l => l.DataVencimento)
            .ToListAsync();

    public async Task<IEnumerable<LancamentoFinanceiro>> ListarPorTipoAsync(string tipo)
        => await _context.LancamentosFinanceiros
            .Include(l => l.Pessoa)
            .Where(l => l.Tipo == tipo)
            .OrderByDescending(l => l.DataVencimento)
            .ToListAsync();

    public async Task<LancamentoFinanceiro?> BuscarPorIdAsync(int id)
        => await _context.LancamentosFinanceiros
            .Include(l => l.Pessoa)
            .FirstOrDefaultAsync(l => l.Id == id);

    public async Task AdicionarAsync(LancamentoFinanceiro lancamento)
        => await _context.LancamentosFinanceiros.AddAsync(lancamento);

    public Task AtualizarAsync(LancamentoFinanceiro lancamento)
    {
        _context.LancamentosFinanceiros.Update(lancamento);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}