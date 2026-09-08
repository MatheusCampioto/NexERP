using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class OrdensCompraController : ControllerBase
{
    private readonly IOrdemCompraService _service;

    public OrdensCompraController(IOrdemCompraService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _service.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var o = await _service.BuscarPorIdAsync(id);
        if (o == null) return NotFound(new { mensagem = "Ordem nao encontrada." });
        return Ok(o);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarOrdemCompraRequest request)
    {
        var itens = request.Itens.Select(i => (i.ProdutoId, i.Descricao, i.Quantidade, i.ValorUnitario)).ToList();
        var o = await _service.CriarAsync(request.FornecedorId, request.SolicitacaoCompraId,
            request.CondicaoPagamentoId, request.DataPrevista, request.Observacao, itens);
        return StatusCode(201, o);
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(int id, [FromBody] AtualizarStatusOrdemRequest request)
    {
        var resultado = await _service.AtualizarStatusAsync(id, request.Status);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var resultado = await _service.CancelarAsync(id);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }
}

public record CriarOrdemCompraRequest(
    int FornecedorId, int? SolicitacaoCompraId, int? CondicaoPagamentoId,
    DateTime? DataPrevista, string? Observacao, List<ItemOrdemCompraRequest> Itens);
public record ItemOrdemCompraRequest(int? ProdutoId, string Descricao, decimal Quantidade, decimal ValorUnitario);
public record AtualizarStatusOrdemRequest(string Status);
