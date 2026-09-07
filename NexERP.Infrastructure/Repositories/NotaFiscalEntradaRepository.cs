using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class NotaFiscalEntradaRepository : BaseRepository<NotaFiscalEntrada>, INotaFiscalEntradaRepository
{
    public NotaFiscalEntradaRepository(AppDbContext context) : base(context) { }

    public override async Task<IEnumerable<NotaFiscalEntrada>> ListarTodosAsync()
        => await _dbSet
            .Include(n => n.OrdemCompra).ThenInclude(o => o.Fornecedor)
            .Include(n => n.Itens).ThenInclude(i => i.Produto)
            .OrderByDescending(n => n.CriadoEm)
            .ToListAsync();

    public override async Task<NotaFiscalEntrada?> BuscarPorIdAsync(int id)
        => await _dbSet
            .Include(n => n.OrdemCompra).ThenInclude(o => o.Fornecedor)
            .Include(n => n.Itens).ThenInclude(i => i.Produto)
            .FirstOrDefaultAsync(n => n.Id == id);
}
