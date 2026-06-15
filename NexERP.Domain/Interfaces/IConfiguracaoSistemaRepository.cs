using NexERP.Domain.Entities;

namespace NexERP.Domain.Interfaces;

public interface IConfiguracaoSistemaRepository
{
    Task<ConfiguracaoSistema?> ObterAsync();
    Task SalvarAsync(ConfiguracaoSistema configuracao);
}