using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public abstract class BaseRepository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

   public virtual async Task<IEnumerable<T>> ListarTodosAsync()
        => await _dbSet.ToListAsync();

    public virtual async Task<T?> BuscarPorIdAsync(int id)
        => await _dbSet.FindAsync(id);

    public async Task AdicionarAsync(T entity)
        => await _dbSet.AddAsync(entity);

    public Task AtualizarAsync(T entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    }

    public async Task<bool> ExisteAsync(int id)
        => await _dbSet.FindAsync(id) != null;
}
