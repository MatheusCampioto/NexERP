using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class CondicoesPagamentoController : ControllerBase
{
    private readonly CondicaoPagamentoService _service;

    public CondicoesPagamentoController(CondicaoPagamentoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _service.ListarTodosAsync());

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CondicaoPagamentoRequest request)
    {
        var condicao = await _service.CriarAsync(request.Nome, request.Descricao,
            request.NumeroParcelas, request.DiasEntreParcelas, request.PrimeiroPagamentoDias);
        return StatusCode(201, condicao);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] CondicaoPagamentoRequest request)
    {
        var atualizado = await _service.AtualizarAsync(id, request.Nome, request.Descricao,
            request.NumeroParcelas, request.DiasEntreParcelas, request.PrimeiroPagamentoDias);
        if (!atualizado) return NotFound(new { mensagem = "Condição não encontrada." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Desativar(int id)
    {
        var desativado = await _service.DesativarAsync(id);
        if (!desativado) return NotFound(new { mensagem = "Condição não encontrada." });
        return NoContent();
    }
}

public record CondicaoPagamentoRequest(
    string Nome,
    string? Descricao,
    int NumeroParcelas,
    int DiasEntreParcelas,
    int PrimeiroPagamentoDias
);