using NexERP.Application.Interfaces;
using NexERP.Domain.Entities;
using NexERP.Domain.Exceptions;
using NexERP.Domain.Interfaces;

namespace NexERP.Application.Services;

public class PessoaService : IPessoaService
{
    private readonly IPessoaRepository _pessoaRepository;
    private readonly IUnitOfWork _unitOfWork;

    public PessoaService(IPessoaRepository pessoaRepository, IUnitOfWork unitOfWork)
    {
        _pessoaRepository = pessoaRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Pessoa>> ListarTodosAsync()
        => await _pessoaRepository.ListarTodosAsync();

    public async Task<Pessoa?> BuscarPorIdAsync(int id)
        => await _pessoaRepository.BuscarPorIdAsync(id);

    public async Task<Pessoa> CriarAsync(PessoaDto dto)
    {
        var pessoa = new Pessoa(
            dto.Nome ?? throw new DomainException("Nome é obrigatório."),
            dto.TipoDocumento,
            dto.Tipo);

        MapearDto(pessoa, dto);
        pessoa.Validar();

        await _pessoaRepository.AdicionarAsync(pessoa);
        await _unitOfWork.CommitAsync();
        return pessoa;
    }

    public async Task<bool> AtualizarAsync(int id, PessoaDto dto)
    {
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(id);
        if (pessoa == null) return false;
        MapearDto(pessoa, dto);
        pessoa.Validar();
        await _pessoaRepository.AtualizarAsync(pessoa);
        await _unitOfWork.CommitAsync();
        return true;
    }

    public async Task<bool> DesativarAsync(int id)
    {
        var pessoa = await _pessoaRepository.BuscarPorIdAsync(id);
        if (pessoa == null) return false;
        pessoa.Desativar();
        await _pessoaRepository.AtualizarAsync(pessoa);
        await _unitOfWork.CommitAsync();
        return true;
    }

    private static Pessoa MapearDto(Pessoa pessoa, PessoaDto dto)
    {
        pessoa.TipoDocumento = dto.TipoDocumento;
        pessoa.Tipo = dto.Tipo;
        pessoa.Funcao = dto.Funcao;
        pessoa.Nome = dto.Nome ?? string.Empty;
        pessoa.CPF = dto.CPF;
        pessoa.RG = dto.RG;
        pessoa.DataNascimento = dto.DataNascimento.HasValue
            ? DateTime.SpecifyKind(dto.DataNascimento.Value, DateTimeKind.Utc)
            : null;
        pessoa.EstadoCivil = dto.EstadoCivil;
        pessoa.Profissao = dto.Profissao;
        pessoa.RazaoSocial = dto.RazaoSocial;
        pessoa.NomeFantasia = dto.NomeFantasia;
        pessoa.CNPJ = dto.CNPJ;
        pessoa.InscricaoEstadual = dto.InscricaoEstadual;
        pessoa.InscricaoMunicipal = dto.InscricaoMunicipal;
        pessoa.NomeContato = dto.NomeContato;
        pessoa.Site = dto.Site;
        pessoa.Email = dto.Email;
        pessoa.Telefone = dto.Telefone;
        pessoa.Celular = dto.Celular;
        pessoa.CEP = dto.CEP;
        pessoa.Endereco = dto.Endereco;
        pessoa.Numero = dto.Numero;
        pessoa.Complemento = dto.Complemento;
        pessoa.Bairro = dto.Bairro;
        pessoa.Cidade = dto.Cidade;
        pessoa.Estado = dto.Estado;
        pessoa.Observacao = dto.Observacao;
        return pessoa;
    }
}

public record PessoaDto(
    string TipoDocumento,
    string Tipo,
    string? Funcao,
    string? Nome,
    string? CPF,
    string? RG,
    DateTime? DataNascimento,
    string? EstadoCivil,
    string? Profissao,
    string? RazaoSocial,
    string? NomeFantasia,
    string? CNPJ,
    string? InscricaoEstadual,
    string? InscricaoMunicipal,
    string? NomeContato,
    string? Site,
    string? Email,
    string? Telefone,
    string? Celular,
    string? CEP,
    string? Endereco,
    string? Numero,
    string? Complemento,
    string? Bairro,
    string? Cidade,
    string? Estado,
    string? Observacao
);