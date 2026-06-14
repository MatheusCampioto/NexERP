using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class OrdemServicoService
{
    private readonly IOrdemServicoRepository _ordemServicoRepository;

    public OrdemServicoService(IOrdemServicoRepository ordemServicoRepository)
    {
        _ordemServicoRepository = ordemServicoRepository;
    }

    public async Task<IEnumerable<OrdemServico>> ListarTodosAsync()
        => await _ordemServicoRepository.ListarTodosAsync();

    public async Task<OrdemServico?> BuscarPorIdAsync(int id)
        => await _ordemServicoRepository.BuscarPorIdAsync(id);

    public async Task<OrdemServico> CriarAsync(int pessoaId, string titulo, string? descricao,
        string prioridade, decimal? valorEstimado, DateTime? dataPrevista,
        string? tecnico, string? observacao,
        List<(string descricao, decimal quantidade, decimal valorUnitario)> itens)
    {
        var os = new OrdemServico
        {
            PessoaId = pessoaId,
            Titulo = titulo,
            Descricao = descricao,
            Prioridade = prioridade,
            ValorEstimado = valorEstimado,
            DataPrevista = dataPrevista.HasValue
                ? DateTime.SpecifyKind(dataPrevista.Value, DateTimeKind.Utc)
                : null,
            Tecnico = tecnico,
            Observacao = observacao
        };

        foreach (var (desc, qtd, valor) in itens)
        {
            os.Itens.Add(new ItemOrdemServico
            {
                Descricao = desc,
                Quantidade = qtd,
                ValorUnitario = valor
            });
        }

        await _ordemServicoRepository.AdicionarAsync(os);
        await _ordemServicoRepository.SalvarAsync();
        return os;
    }

    public async Task<(bool sucesso, string mensagem)> AtualizarStatusAsync(int id, string novoStatus)
    {
        var os = await _ordemServicoRepository.BuscarPorIdAsync(id);
        if (os == null) return (false, "Ordem de serviço não encontrada.");

        os.Status = novoStatus;
        if (novoStatus == "Concluida")
            os.DataConclusao = DateTime.UtcNow;

        await _ordemServicoRepository.AtualizarAsync(os);
        await _ordemServicoRepository.SalvarAsync();
        return (true, $"Status atualizado para {novoStatus}.");
    }

    public async Task<(bool sucesso, string mensagem)> FinalizarAsync(int id, decimal valorFinal, string? observacao)
    {
        var os = await _ordemServicoRepository.BuscarPorIdAsync(id);
        if (os == null) return (false, "Ordem de serviço não encontrada.");
        if (os.Status == "Cancelada") return (false, "Ordem cancelada não pode ser finalizada.");

        os.Status = "Concluida";
        os.ValorFinal = valorFinal;
        os.DataConclusao = DateTime.UtcNow;
        if (observacao != null) os.Observacao = observacao;

        await _ordemServicoRepository.AtualizarAsync(os);
        await _ordemServicoRepository.SalvarAsync();
        return (true, "Ordem de serviço finalizada com sucesso.");
    }

    public async Task<(bool sucesso, string mensagem)> CancelarAsync(int id)
    {
        var os = await _ordemServicoRepository.BuscarPorIdAsync(id);
        if (os == null) return (false, "Ordem de serviço não encontrada.");
        if (os.Status == "Concluida") return (false, "Ordem concluída não pode ser cancelada.");

        os.Status = "Cancelada";
        await _ordemServicoRepository.AtualizarAsync(os);
        await _ordemServicoRepository.SalvarAsync();
        return (true, "Ordem de serviço cancelada.");
    }
}