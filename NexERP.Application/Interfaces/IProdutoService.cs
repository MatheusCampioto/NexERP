using NexERP.Application.Services;
using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IProdutoService
{
    Task<IEnumerable<Produto>> ListarTodosAsync();
    Task<Produto?> BuscarPorIdAsync(int id);
    Task<Produto> CriarAsync(ProdutoDto dto);
    Task<bool> AtualizarAsync(int id, ProdutoDto dto);
    Task<bool> DesativarAsync(int id);
}
