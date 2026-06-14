using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class SolicitacaoCompraRepository : ISolicitacaoCompraRepository
{
    private readonly AppDbContext _context;

    public SolicitacaoCompraRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SolicitacaoCompra>> ListarTodosAsync()
        => await _context.SolicitacoesCompra
            .Include(s => s.Usuario)
            .Include(s => s.Itens).ThenInclude(i => i.Produto)
            .OrderByDescending(s => s.CriadoEm)
            .ToListAsync();

    public async Task<SolicitacaoCompra?> BuscarPorIdAsync(int id)
        => await _context.SolicitacoesCompra
            .Include(s => s.Usuario)
            .Include(s => s.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(s => s.Id == id);

    public async Task AdicionarAsync(SolicitacaoCompra solicitacao)
        => await _context.SolicitacoesCompra.AddAsync(solicitacao);

    public Task AtualizarAsync(SolicitacaoCompra solicitacao)
    {
        _context.SolicitacoesCompra.Update(solicitacao);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}