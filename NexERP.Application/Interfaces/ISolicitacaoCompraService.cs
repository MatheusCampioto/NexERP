using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface ISolicitacaoCompraService
{
    Task<IEnumerable<SolicitacaoCompra>> ListarTodosAsync();
    Task<SolicitacaoCompra?> BuscarPorIdAsync(int id);
    Task<SolicitacaoCompra> CriarAsync(int usuarioId, string? observacao,
        List<(int? produtoId, string descricao, decimal quantidade, string? unidade, string? obs)> itens);
    Task<(bool sucesso, string mensagem)> AprovarAsync(int id);
    Task<(bool sucesso, string mensagem)> ReprovarAsync(int id, string motivo);
    Task<(bool sucesso, string mensagem)> CancelarAsync(int id);
}
