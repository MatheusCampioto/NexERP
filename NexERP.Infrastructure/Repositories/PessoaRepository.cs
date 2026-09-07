using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class PessoaRepository : BaseRepository<Pessoa>, IPessoaRepository
{
    public PessoaRepository(AppDbContext context) : base(context) { }

    // Override — filtra apenas ativos
    public override async Task<IEnumerable<Pessoa>> ListarTodosAsync()
        => await _dbSet.Where(p => p.Ativo).ToListAsync();

    public async Task<IEnumerable<Pessoa>> ListarPorTipoAsync(string tipo)
        => await _dbSet.Where(p => p.Ativo && p.Tipo == tipo).ToListAsync();

    public async Task<Pessoa?> BuscarPorCpfAsync(string cpf)
        => await _dbSet.FirstOrDefaultAsync(p => p.CPF == cpf);

    public async Task<Pessoa?> BuscarPorCnpjAsync(string cnpj)
        => await _dbSet.FirstOrDefaultAsync(p => p.CNPJ == cnpj);
}