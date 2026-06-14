using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class OrdemServicoRepository : IOrdemServicoRepository
{
    private readonly AppDbContext _context;

    public OrdemServicoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrdemServico>> ListarTodosAsync()
        => await _context.OrdensServico
            .Include(o => o.Pessoa)
            .Include(o => o.Itens)
            .OrderByDescending(o => o.CriadoEm)
            .ToListAsync();

    public async Task<OrdemServico?> BuscarPorIdAsync(int id)
        => await _context.OrdensServico
            .Include(o => o.Pessoa)
            .Include(o => o.Itens)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task AdicionarAsync(OrdemServico ordemServico)
        => await _context.OrdensServico.AddAsync(ordemServico);

    public Task AtualizarAsync(OrdemServico ordemServico)
    {
        _context.OrdensServico.Update(ordemServico);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}