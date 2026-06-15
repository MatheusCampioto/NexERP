using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class ConfiguracaoSistemaService
{
    private readonly IConfiguracaoSistemaRepository _repository;

    public ConfiguracaoSistemaService(IConfiguracaoSistemaRepository repository)
    {
        _repository = repository;
    }

    public async Task<ConfiguracaoSistema?> ObterAsync()
        => await _repository.ObterAsync();

    public async Task SalvarAsync(ConfiguracaoSistema configuracao)
        => await _repository.SalvarAsync(configuracao);
}