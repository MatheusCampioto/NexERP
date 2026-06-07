using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class PessoaRepository : IPessoaRepository
{
    private readonly AppDbContext _context;

    public PessoaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Pessoa>> ListarTodosAsync()
        => await _context.Pessoas.Where(p => p.Ativo).ToListAsync();

    public async Task<Pessoa?> BuscarPorIdAsync(int id)
        => await _context.Pessoas.FindAsync(id);

    public async Task AdicionarAsync(Pessoa pessoa)
        => await _context.Pessoas.AddAsync(pessoa);

    public Task AtualizarAsync(Pessoa pessoa)
{
    _context.Pessoas.Update(pessoa);
    return Task.CompletedTask;
}

    public async Task<bool> ExisteAsync(int id)
        => await _context.Pessoas.AnyAsync(p => p.Id == id);

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}