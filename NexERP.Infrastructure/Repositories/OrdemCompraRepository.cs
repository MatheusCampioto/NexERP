using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class OrdemCompraRepository : IOrdemCompraRepository
{
    private readonly AppDbContext _context;

    public OrdemCompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<OrdemCompra>> ListarTodosAsync()
        => await _context.OrdensCompra
            .Include(o => o.Fornecedor)
            .Include(o => o.CondicaoPagamento)
            .Include(o => o.Itens).ThenInclude(i => i.Produto)
            .OrderByDescending(o => o.CriadoEm)
            .ToListAsync();

    public async Task<OrdemCompra?> BuscarPorIdAsync(int id)
        => await _context.OrdensCompra
            .Include(o => o.Fornecedor)
            .Include(o => o.CondicaoPagamento)
            .Include(o => o.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(o => o.Id == id);

    public async Task AdicionarAsync(OrdemCompra ordemCompra)
        => await _context.OrdensCompra.AddAsync(ordemCompra);

    public Task AtualizarAsync(OrdemCompra ordemCompra)
    {
        _context.OrdensCompra.Update(ordemCompra);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}