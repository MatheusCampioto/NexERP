using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IUsuarioRepository
{
    Task<IEnumerable<Usuario>> ListarTodosAsync();
    Task<Usuario?> BuscarPorEmailAsync(string email);
    Task<Usuario?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Usuario usuario);
    Task SalvarAsync();
}