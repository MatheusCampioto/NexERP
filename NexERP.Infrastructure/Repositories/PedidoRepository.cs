using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class PedidoRepository : IPedidoRepository
{
    private readonly AppDbContext _context;

    public PedidoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pedido>> ListarTodosAsync()
        => await _context.Pedidos
            .Include(p => p.Pessoa)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .OrderByDescending(p => p.CriadoEm)
            .ToListAsync();

    public async Task<Pedido?> BuscarPorIdAsync(int id)
        => await _context.Pedidos
            .Include(p => p.Pessoa)
            .Include(p => p.Itens)
            .ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(p => p.Id == id);

    public async Task AdicionarAsync(Pedido pedido)
        => await _context.Pedidos.AddAsync(pedido);

    public Task AtualizarAsync(Pedido pedido)
    {
        _context.Pedidos.Update(pedido);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}