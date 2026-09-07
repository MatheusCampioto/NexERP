using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IMovimentacaoEstoqueRepository : IRepository<MovimentacaoEstoque>
{
    Task<IEnumerable<MovimentacaoEstoque>> ListarPorProdutoAsync(int produtoId);
}
