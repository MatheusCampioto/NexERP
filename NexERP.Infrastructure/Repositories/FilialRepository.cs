using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class FilialRepository : IFilialRepository
{
    private readonly AppDbContext _context;

    public FilialRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Filial>> ListarTodosAsync()
        => await _context.Filiais.Include(f => f.Pessoa).OrderBy(f => f.Numero).ToListAsync();

    public async Task<Filial?> BuscarPorIdAsync(int id)
        => await _context.Filiais.Include(f => f.Pessoa).FirstOrDefaultAsync(f => f.Id == id);

    public async Task AdicionarAsync(Filial filial)
        => await _context.Filiais.AddAsync(filial);

    public async Task AtualizarAsync(Filial filial)
        => _context.Filiais.Update(filial);

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}