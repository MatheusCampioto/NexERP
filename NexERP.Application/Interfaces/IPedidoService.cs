using NexERP.Domain.Entities;
using NexERP.Domain.Enums;

namespace NexERP.Application.Interfaces;

public interface IPedidoService
{
    Task<IEnumerable<Pedido>> ListarTodosAsync();
    Task<Pedido?> BuscarPorIdAsync(int id);
    Task<(bool sucesso, string mensagem, Pedido? pedido)> CriarAsync(
        int pessoaId, string? observacao, int? condicaoPagamentoId,
        FormaPagamento? formaPagamento, decimal desconto,
        List<(int produtoId, int quantidade, decimal desconto)> itens);
    Task<(bool sucesso, string mensagem)> AtualizarAsync(
        int id, int pessoaId, string? observacao, int? condicaoPagamentoId,
        FormaPagamento? formaPagamento, decimal desconto,
        List<(int produtoId, int quantidade, decimal desconto)> itens);
    Task<(bool sucesso, string mensagem)> AvancarStatusAsync(int id);
    Task<(bool sucesso, string mensagem)> CancelarAsync(int id);
}