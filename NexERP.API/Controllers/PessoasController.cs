using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PessoasController : ControllerBase
{
    private readonly PessoaService _pessoaService;

    public PessoasController(PessoaService pessoaService)
    {
        _pessoaService = pessoaService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _pessoaService.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var pessoa = await _pessoaService.BuscarPorIdAsync(id);
        if (pessoa == null)
            return NotFound(new { mensagem = "Pessoa não encontrada." });
        return Ok(pessoa);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] PessoaRequest request)
    {
        var pessoa = await _pessoaService.CriarAsync(request.ToDto());
        return StatusCode(201, pessoa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] PessoaRequest request)
    {
        var atualizado = await _pessoaService.AtualizarAsync(id, request.ToDto());
        if (!atualizado)
            return NotFound(new { mensagem = "Pessoa não encontrada." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var desativado = await _pessoaService.DesativarAsync(id);
        if (!desativado)
            return NotFound(new { mensagem = "Pessoa não encontrada." });
        return NoContent();
    }
}

public record PessoaRequest(
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
)
{
    public PessoaDto ToDto() => new(
        TipoDocumento, Tipo, Funcao, Nome, CPF, RG, DataNascimento,
        EstadoCivil, Profissao, RazaoSocial, NomeFantasia, CNPJ,
        InscricaoEstadual, InscricaoMunicipal, NomeContato, Site,
        Email, Telefone, Celular, CEP, Endereco, Numero, Complemento,
        Bairro, Cidade, Estado, Observacao
    );
}