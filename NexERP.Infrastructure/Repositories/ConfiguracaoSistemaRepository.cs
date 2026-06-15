using Microsoft.EntityFrameworkCore;
using NexERP.Domain.Entities;
using NexERP.Domain.Interfaces;
using NexERP.Infrastructure.Data;

namespace NexERP.Infrastructure.Repositories;

public class ConfiguracaoSistemaRepository : IConfiguracaoSistemaRepository
{
    private readonly AppDbContext _context;

    public ConfiguracaoSistemaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ConfiguracaoSistema?> ObterAsync()
        => await _context.ConfiguracoesSistema.FirstOrDefaultAsync();

    public async Task SalvarAsync(ConfiguracaoSistema configuracao)
    {
        var existente = await _context.ConfiguracoesSistema.FirstOrDefaultAsync();
        if (existente == null)
        {
            configuracao.AtualizadoEm = DateTime.UtcNow;
            await _context.ConfiguracoesSistema.AddAsync(configuracao);
        }
        else
        {
            existente.NomeEmpresa = configuracao.NomeEmpresa;
            existente.NomeFantasia = configuracao.NomeFantasia;
            existente.CNPJ = configuracao.CNPJ;
            existente.InscricaoEstadual = configuracao.InscricaoEstadual;
            existente.InscricaoMunicipal = configuracao.InscricaoMunicipal;
            existente.RegimeTributario = configuracao.RegimeTributario;
            existente.Email = configuracao.Email;
            existente.Telefone = configuracao.Telefone;
            existente.Site = configuracao.Site;
            existente.CEP = configuracao.CEP;
            existente.Endereco = configuracao.Endereco;
            existente.Numero = configuracao.Numero;
            existente.Complemento = configuracao.Complemento;
            existente.Bairro = configuracao.Bairro;
            existente.Cidade = configuracao.Cidade;
            existente.Estado = configuracao.Estado;
            existente.CFOP_PadraoVenda = configuracao.CFOP_PadraoVenda;
            existente.CFOP_PadraoCompra = configuracao.CFOP_PadraoCompra;
            existente.AliquotaICMS_Padrao = configuracao.AliquotaICMS_Padrao;
            existente.AliquotaPIS_Padrao = configuracao.AliquotaPIS_Padrao;
            existente.AliquotaCOFINS_Padrao = configuracao.AliquotaCOFINS_Padrao;
            existente.MoedaSimbolo = configuracao.MoedaSimbolo;
            existente.AtualizadoEm = DateTime.UtcNow;
        }
        await _context.SaveChangesAsync();
    }
}