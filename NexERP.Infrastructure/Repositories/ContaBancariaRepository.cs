using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class ContaBancariaRepository : IContaBancariaRepository
{
    private readonly AppDbContext _context;

    public ContaBancariaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ContaBancaria>> ListarTodosAsync()
        => await _context.ContasBancarias.Where(c => c.Ativa).ToListAsync();

    public async Task<ContaBancaria?> BuscarPorIdAsync(int id)
        => await _context.ContasBancarias.FindAsync(id);

    public async Task AdicionarAsync(ContaBancaria conta)
        => await _context.ContasBancarias.AddAsync(conta);

    public Task AtualizarAsync(ContaBancaria conta)
    {
        _context.ContasBancarias.Update(conta);
        return Task.CompletedTask;
    }

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}