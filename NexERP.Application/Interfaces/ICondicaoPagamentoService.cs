using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface ICondicaoPagamentoService
{
    Task<IEnumerable<CondicaoPagamento>> ListarTodosAsync();
    Task<CondicaoPagamento> CriarAsync(string nome, string? descricao,
        int numeroParcelas, int diasEntreParcelas, int primeiroPagamentoDias);
    Task<bool> AtualizarAsync(int id, string nome, string? descricao,
        int numeroParcelas, int diasEntreParcelas, int primeiroPagamentoDias);
    Task<bool> DesativarAsync(int id);
}
