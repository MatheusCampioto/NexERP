using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IOrdemCompraService
{
    Task<IEnumerable<OrdemCompra>> ListarTodosAsync();
    Task<OrdemCompra?> BuscarPorIdAsync(int id);
    Task<OrdemCompra> CriarAsync(int fornecedorId, int? solicitacaoCompraId,
        int? condicaoPagamentoId, DateTime? dataPrevista, string? observacao,
        List<(int? produtoId, string descricao, decimal quantidade, decimal valorUnitario)> itens);
    Task<(bool sucesso, string mensagem)> AtualizarStatusAsync(int id, string novoStatus);
    Task<(bool sucesso, string mensagem)> CancelarAsync(int id);
}
