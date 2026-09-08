using NexERP.Domain.Entities;

namespace NexERP.Application.Interfaces;

public interface INotaFiscalEntradaService
{
    Task<IEnumerable<NotaFiscalEntrada>> ListarTodosAsync();
    Task<NotaFiscalEntrada?> BuscarPorIdAsync(int id);
    Task<NotaFiscalEntrada> CriarAsync(int ordemCompraId, string numeroNF,
        string? serie, string? chaveAcesso, DateTime dataEmissao,
        decimal valorProdutos, decimal valorFrete, decimal valorImpostos, string? observacao,
        List<(int? produtoId, string descricao, decimal quantidade, decimal valorUnitario)> itens);
    Task<(bool sucesso, string mensagem)> DarEntradaEstoqueAsync(int nfId);
}
