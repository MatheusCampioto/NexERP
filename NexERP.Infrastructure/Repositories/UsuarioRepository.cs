using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Usuario?> BuscarPorEmailAsync(string email)
        => await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<Usuario?> BuscarPorIdAsync(int id)
        => await _context.Usuarios.FindAsync(id);

    public async Task AdicionarAsync(Usuario usuario)
        => await _context.Usuarios.AddAsync(usuario);

    public async Task SalvarAsync()
        => await _context.SaveChangesAsync();
}