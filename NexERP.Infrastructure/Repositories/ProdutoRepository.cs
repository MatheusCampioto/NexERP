using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private readonly AppDbContext _context;

    public ProdutoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Produto>> ListarTodosAsync()
        => await _context.Produtos.Where(p => p.Ativo).ToListAsync();

    public async Task<Produto?> BuscarPorIdAsync(int id)
        => await _context.Produtos.FindAsync(id);

    public async Task AdicionarAsync(Produto produto)
        => await _context.Produtos.AddAsync(produto);

    public Task AtualizarAsync(Produto produto)
    {
        _context.Produtos.Update(produto);
        return Task.CompletedTask;
    }

    public async Task<bool> ExisteAsync(int id)
        => await _context.Produtos.AnyAsync(p => p.Id == id);

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}