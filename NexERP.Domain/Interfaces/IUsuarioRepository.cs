using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario?> BuscarPorEmailAsync(string email);
}