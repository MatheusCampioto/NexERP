using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface IEstoqueService
{
    Task<IEnumerable<MovimentacaoEstoque>> ListarMovimentacoesPorProdutoAsync(int produtoId);
    Task<IEnumerable<Produto>> ListarProdutosEstoqueBaixoAsync();
    Task<(bool sucesso, string mensagem)> MovimentarAsync(int produtoId, string tipo, int quantidade, string? observacao);
    Task<(bool sucesso, string mensagem)> AjustarInventarioAsync(int produtoId, int quantidadeReal, string? observacao);
}
