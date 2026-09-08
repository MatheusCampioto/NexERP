using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;
using System.Security.Claims;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SolicitacoesCompraController : ControllerBase
{
    private readonly ISolicitacaoCompraService _service;

    public SolicitacoesCompraController(ISolicitacaoCompraService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _service.ListarTodosAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var s = await _service.BuscarPorIdAsync(id);
        if (s == null) return NotFound(new { mensagem = "Solicitacao nao encontrada." });
        return Ok(s);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarSolicitacaoRequest request)
    {
        var usuarioId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var itens = request.Itens.Select(i => (i.ProdutoId, i.Descricao, i.Quantidade, i.Unidade, i.Observacao)).ToList();
        var s = await _service.CriarAsync(usuarioId, request.Observacao, itens);
        return StatusCode(201, s);
    }

    [HttpPatch("{id}/aprovar")]
    public async Task<IActionResult> Aprovar(int id)
    {
        var resultado = await _service.AprovarAsync(id);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/reprovar")]
    public async Task<IActionResult> Reprovar(int id, [FromBody] ReprovarRequest request)
    {
        var resultado = await _service.ReprovarAsync(id, request.Motivo);
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

public record CriarSolicitacaoRequest(string? Observacao, List<ItemSolicitacaoRequest> Itens);
public record ItemSolicitacaoRequest(int? ProdutoId, string Descricao, decimal Quantidade, string? Unidade, string? Observacao);
public record ReprovarRequest(string Motivo);
