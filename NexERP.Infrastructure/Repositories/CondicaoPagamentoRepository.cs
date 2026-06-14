using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class CondicaoPagamentoRepository : ICondicaoPagamentoRepository
{
    private readonly AppDbContext _context;

    public CondicaoPagamentoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<CondicaoPagamento>> ListarTodosAsync()
        => await _context.CondicoesPagamento.Where(c => c.Ativa).ToListAsync();

    public async Task<CondicaoPagamento?> BuscarPorIdAsync(int id)
        => await _context.CondicoesPagamento.FindAsync(id);

    public async Task AdicionarAsync(CondicaoPagamento condicao)
        => await _context.CondicoesPagamento.AddAsync(condicao);

    public Task AtualizarAsync(CondicaoPagamento condicao)
    {
        _context.CondicoesPagamento.Update(condicao);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}