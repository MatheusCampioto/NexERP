using NexERP.Application.Services;
using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IPessoaService
{
    Task<IEnumerable<Pessoa>> ListarTodosAsync();
    Task<Pessoa?> BuscarPorIdAsync(int id);
    Task<Pessoa> CriarAsync(PessoaDto dto);
    Task<bool> AtualizarAsync(int id, PessoaDto dto);
    Task<bool> DesativarAsync(int id);
}