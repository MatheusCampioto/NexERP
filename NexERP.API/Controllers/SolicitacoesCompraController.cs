using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;
using System.Security.Claims;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SolicitacoesCompraController : ControllerBase
{
    private readonly SolicitacaoCompraService _service;

    public SolicitacoesCompraController(SolicitacaoCompraService service)
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
        if (s == null) return NotFound(new { mensagem = "Solicitação não encontrada." });
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
        var (sucesso, mensagem) = await _service.AprovarAsync(id);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }

    [HttpPatch("{id}/reprovar")]
    public async Task<IActionResult> Reprovar(int id, [FromBody] ReprovarRequest request)
    {
        var (sucesso, mensagem) = await _service.ReprovarAsync(id, request.Motivo);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var (sucesso, mensagem) = await _service.CancelarAsync(id);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }
}

public record CriarSolicitacaoRequest(
    string? Observacao,
    List<ItemSolicitacaoRequest> Itens
);

public record ItemSolicitacaoRequest(
    int? ProdutoId,
    string Descricao,
    decimal Quantidade,
    string? Unidade,
    string? Observacao
);

public record ReprovarRequest(string Motivo);