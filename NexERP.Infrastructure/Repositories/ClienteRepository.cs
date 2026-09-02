using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.data;

namespace NexERP.Infrastructure.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Cliente> ObterPorIdAsync(Guid id)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Cliente>> ObterTodosAsync()
        {
            return await _context.Clientes
                .OrderBy(c => c.RazaoSocial)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterAtivosAsync()
        {
            return await _context.Clientes
                .Where(c => c.Ativo)
                .OrderBy(c => c.RazaoSocial)
                .ToListAsync();
        }

        public async Task<Cliente> ObterPorCnpjCpfAsync(string cnpjCpf)
        {
            return await _context.Clientes
                .FirstOrDefaultAsync(c => c.CnpjCpf == cnpjCpf);
        }

        public async Task<IEnumerable<Cliente>> BuscarPorRazaoSocialAsync(string razaoSocial)
        {
            return await _context.Clientes
                .Where(c => c.RazaoSocial.Contains(razaoSocial))
                .OrderBy(c => c.RazaoSocial)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterPorCidadeAsync(string cidade)
        {
            return await _context.Clientes
                .Where(c => c.Cidade == cidade)
                .OrderBy(c => c.RazaoSocial)
                .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterPorUFAsync(string uf)
        {
            return await _context.Clientes
                .Where(c => c.UF == uf)
                .OrderBy(c => c.RazaoSocial)
                .ToListAsync();
        }

        public async Task<bool> ExistePorCnpjCpfAsync(string cnpjCpf, Guid? ignorarId = null)
        {
            return await _context.Clientes
                .AnyAsync(c => c.CnpjCpf == cnpjCpf && (!ignorarId.HasValue || c.Id != ignorarId.Value));
        }

        public async Task AdicionarAsync(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cliente cliente)
        {
            if (cliente == null) throw new ArgumentNullException(nameof(cliente));
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var cliente = await ObterPorIdAsync(id);
            if (cliente == null) throw new KeyNotFoundException($"Cliente com ID {id} não encontrado.");
            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
        }

        public async Task<int> TotalClientesAtivosAsync()
        {
            return await _context.Clientes.CountAsync(c => c.Ativo);
        }

        public async Task<IEnumerable<Cliente>> ObterComLimiteCreditoAcimaDeAsync(decimal valor)
        {
            return await _context.Clientes
                .Where(c => c.LimiteCredito > valor && c.Ativo)
                .OrderByDescending(c => c.LimiteCredito)
                .ToListAsync();
        }
    }
}