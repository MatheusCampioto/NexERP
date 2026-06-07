using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceiroController : ControllerBase
{
    private readonly FinanceiroService _financeiroService;

    public FinanceiroController(FinanceiroService financeiroService)
    {
        _financeiroService = financeiroService;
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
        => Ok(await _financeiroService.ListarTodosAsync());

    [HttpGet("pagar")]
    public async Task<IActionResult> ContasAPagar()
        => Ok(await _financeiroService.ListarContasAPagarAsync());

    [HttpGet("receber")]
    public async Task<IActionResult> ContasAReceber()
        => Ok(await _financeiroService.ListarContasAReceberAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var lancamento = await _financeiroService.BuscarPorIdAsync(id);
        if (lancamento == null)
            return NotFound(new { mensagem = "Lançamento não encontrado." });
        return Ok(lancamento);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] LancamentoRequest request)
    {
        var lancamento = await _financeiroService.CriarAsync(
            request.Tipo, request.Descricao, request.Valor,
            request.DataVencimento, request.Categoria, request.PessoaId);
        return StatusCode(201, lancamento);
    }

    [HttpPatch("{id}/baixar")]
    public async Task<IActionResult> Baixar(int id)
    {
        var (sucesso, mensagem) = await _financeiroService.BaixarAsync(id);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var (sucesso, mensagem) = await _financeiroService.CancelarAsync(id);
        if (!sucesso) return BadRequest(new { mensagem });
        return Ok(new { mensagem });
    }
}

public record LancamentoRequest(
    string Tipo,
    string Descricao,
    decimal Valor,
    DateTime DataVencimento,
    string? Categoria,
    int? PessoaId
);