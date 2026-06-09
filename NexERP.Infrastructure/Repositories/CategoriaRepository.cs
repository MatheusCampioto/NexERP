using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext _context;

    public CategoriaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Categoria>> ListarTodosAsync()
        => await _context.Categorias.Where(c => c.Ativa).ToListAsync();

    public async Task<Categoria?> BuscarPorIdAsync(int id)
        => await _context.Categorias.FindAsync(id);

    public async Task AdicionarAsync(Categoria categoria)
        => await _context.Categorias.AddAsync(categoria);

    public Task AtualizarAsync(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}