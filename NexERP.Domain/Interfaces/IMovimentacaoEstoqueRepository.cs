using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IMovimentacaoEstoqueRepository
{
    Task<IEnumerable<MovimentacaoEstoque>> ListarPorProdutoAsync(int produtoId);
    Task AdicionarAsync(MovimentacaoEstoque movimentacao);
    Task SalvarAsync();
}