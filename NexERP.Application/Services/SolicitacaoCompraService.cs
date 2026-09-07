using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class SolicitacaoCompraService
{
    private readonly ISolicitacaoCompraRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public SolicitacaoCompraService(ISolicitacaoCompraRepository repository, IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SolicitacaoCompra>> ListarTodosAsync()
        => await _repository.ListarTodosAsync();

    public async Task<SolicitacaoCompra?> BuscarPorIdAsync(int id)
        => await _repository.BuscarPorIdAsync(id);

    public async Task<SolicitacaoCompra> CriarAsync(int usuarioId, string? observacao,
        List<(int? produtoId, string descricao, decimal quantidade, string? unidade, string? obs)> itens)
    {
        var solicitacao = new SolicitacaoCompra
        {
            UsuarioId = usuarioId,
            Observacao = observacao,
            Status = "Pendente"
        };

        foreach (var (produtoId, descricao, quantidade, unidade, obs) in itens)
        {
            solicitacao.Itens.Add(new ItemSolicitacaoCompra
            {
                ProdutoId = produtoId,
                Descricao = descricao,
                Quantidade = quantidade,
                Unidade = unidade,
                Observacao = obs
            });
        }

        await _repository.AdicionarAsync(solicitacao);
        await _unitOfWork.CommitAsync();
        return solicitacao;
    }

    public async Task<(bool sucesso, string mensagem)> AprovarAsync(int id)
    {
        var solicitacao = await _repository.BuscarPorIdAsync(id);
        if (solicitacao == null) return (false, "Solicitacao nao encontrada.");
        if (solicitacao.Status != "Pendente") return (false, "Apenas solicitacoes pendentes podem ser aprovadas.");
        solicitacao.Status = "Aprovada";
        await _repository.AtualizarAsync(solicitacao);
        await _unitOfWork.CommitAsync();
        return (true, "Solicitacao aprovada.");
    }

    public async Task<(bool sucesso, string mensagem)> ReprovarAsync(int id, string motivo)
    {
        var solicitacao = await _repository.BuscarPorIdAsync(id);
        if (solicitacao == null) return (false, "Solicitacao nao encontrada.");
        if (solicitacao.Status != "Pendente") return (false, "Apenas solicitacoes pendentes podem ser reprovadas.");
        solicitacao.Status = "Reprovada";
        solicitacao.MotivoReprovacao = motivo;
        await _repository.AtualizarAsync(solicitacao);
        await _unitOfWork.CommitAsync();
        return (true, "Solicitacao reprovada.");
    }

    public async Task<(bool sucesso, string mensagem)> CancelarAsync(int id)
    {
        var solicitacao = await _repository.BuscarPorIdAsync(id);
        if (solicitacao == null) return (false, "Solicitacao nao encontrada.");
        if (solicitacao.Status == "Aprovada") return (false, "Solicitacao aprovada nao pode ser cancelada.");
        solicitacao.Status = "Cancelada";
        await _repository.AtualizarAsync(solicitacao);
        await _unitOfWork.CommitAsync();
        return (true, "Solicitacao cancelada.");
    }
}
