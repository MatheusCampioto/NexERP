using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ContasBancariasController : ControllerBase
{
    private readonly ContaBancariaService _contaService;

    public ContasBancariasController(ContaBancariaService contaService)
    {
        _contaService = contaService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _contaService.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var conta = await _contaService.BuscarPorIdAsync(id);
        if (conta == null)
            return NotFound(new { mensagem = "Conta não encontrada." });
        return Ok(conta);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] ContaBancariaRequest request)
    {
        var conta = await _contaService.CriarAsync(
            request.Nome, request.Banco, request.Agencia,
            request.NumeroConta, request.SaldoInicial);
        return StatusCode(201, conta);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] ContaBancariaRequest request)
    {
        var atualizado = await _contaService.AtualizarAsync(
            id, request.Nome, request.Banco, request.Agencia, request.NumeroConta);
        if (!atualizado)
            return NotFound(new { mensagem = "Conta não encontrada." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var desativado = await _contaService.DesativarAsync(id);
        if (!desativado)
            return NotFound(new { mensagem = "Conta não encontrada." });
        return NoContent();
    }
}

public record ContaBancariaRequest(
    string Nome,
    string? Banco,
    string? Agencia,
    string? NumeroConta,
    decimal SaldoInicial
);