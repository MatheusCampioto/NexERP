using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class NotaFiscalEntradaRepository : INotaFiscalEntradaRepository
{
    private readonly AppDbContext _context;

    public NotaFiscalEntradaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<NotaFiscalEntrada>> ListarTodosAsync()
        => await _context.NotasFiscaisEntrada
            .Include(n => n.OrdemCompra).ThenInclude(o => o.Fornecedor)
            .Include(n => n.Itens).ThenInclude(i => i.Produto)
            .OrderByDescending(n => n.CriadoEm)
            .ToListAsync();

    public async Task<NotaFiscalEntrada?> BuscarPorIdAsync(int id)
        => await _context.NotasFiscaisEntrada
            .Include(n => n.OrdemCompra).ThenInclude(o => o.Fornecedor)
            .Include(n => n.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(n => n.Id == id);

    public async Task AdicionarAsync(NotaFiscalEntrada nf)
        => await _context.NotasFiscaisEntrada.AddAsync(nf);

    public Task AtualizarAsync(NotaFiscalEntrada nf)
    {
        _context.NotasFiscaisEntrada.Update(nf);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}