using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdensServicoController : ControllerBase
{
    private readonly IOrdemServicoService _ordemServicoService;

    public OrdensServicoController(IOrdemServicoService ordemServicoService)
    {
        _ordemServicoService = ordemServicoService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _ordemServicoService.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var os = await _ordemServicoService.BuscarPorIdAsync(id);
        if (os == null) return NotFound(new { mensagem = "Ordem de servico nao encontrada." });
        return Ok(os);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarOrdemServicoRequest request)
    {
        var itens = request.Itens.Select(i => (i.Descricao, i.Quantidade, i.ValorUnitario)).ToList();
        var os = await _ordemServicoService.CriarAsync(
            request.PessoaId, request.Titulo, request.Descricao,
            request.Prioridade, request.ValorEstimado, request.DataPrevista,
            request.Tecnico, request.Observacao, itens);
        return StatusCode(201, os);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusRequest request)
    {
        var resultado = await _ordemServicoService.AtualizarStatusAsync(id, request.Status);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/finalizar")]
    public async Task<IActionResult> Finalizar(int id, [FromBody] FinalizarOrdemRequest request)
    {
        var resultado = await _ordemServicoService.FinalizarAsync(id, request.ValorFinal, request.Observacao);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var resultado = await _ordemServicoService.CancelarAsync(id);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }
}

public record CriarOrdemServicoRequest(
    int PessoaId, string Titulo, string? Descricao, string Prioridade,
    decimal? ValorEstimado, DateTime? DataPrevista, string? Tecnico,
    string? Observacao, List<ItemOrdemServicoRequest> Itens);

public record ItemOrdemServicoRequest(string Descricao, decimal Quantidade, decimal ValorUnitario);
public record AtualizarStatusRequest(string Status);
public record FinalizarOrdemRequest(decimal ValorFinal, string? Observacao);
