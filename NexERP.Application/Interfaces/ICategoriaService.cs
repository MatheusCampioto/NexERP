using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface ICategoriaService
{
    Task<IEnumerable<Categoria>> ListarTodosAsync();
    Task<Categoria> CriarAsync(string nome, string? descricao);
    Task<bool> AtualizarAsync(int id, string nome, string? descricao);
    Task<bool> DesativarAsync(int id);
}
