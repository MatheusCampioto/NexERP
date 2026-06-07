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
    {
        var pessoas = await _pessoaService.ListarTodosAsync();
        return Ok(pessoas);
    }

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
        var pessoa = await _pessoaService.CriarAsync(
            request.Nome, request.Tipo, request.CpfCnpj,
            request.Email, request.Telefone, request.Endereco,
            request.Cidade, request.Estado, request.Cep);

        return StatusCode(201, pessoa);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] PessoaRequest request)
    {
        var atualizado = await _pessoaService.AtualizarAsync(
            id, request.Nome, request.Tipo, request.CpfCnpj,
            request.Email, request.Telefone, request.Endereco,
            request.Cidade, request.Estado, request.Cep);

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
    string Nome,
    string Tipo,
    string? CpfCnpj,
    string? Email,
    string? Telefone,
    string? Endereco,
    string? Cidade,
    string? Estado,
    string? Cep
);