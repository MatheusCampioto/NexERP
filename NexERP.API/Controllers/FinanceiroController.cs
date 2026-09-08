using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Interfaces;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class FinanceiroController : ControllerBase
{
    private readonly IFinanceiroService _financeiroService;

    public FinanceiroController(IFinanceiroService financeiroService)
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
            return NotFound(new { mensagem = "Lancamento nao encontrado." });
        return Ok(lancamento);
    }

    [HttpGet("fluxo-caixa")]
    public async Task<IActionResult> FluxoDeCaixa([FromQuery] DateTime inicio, [FromQuery] DateTime fim)
        => Ok(await _financeiroService.FluxoDeCaixaAsync(inicio, fim));

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] LancamentoRequest request)
    {
        var lancamentos = await _financeiroService.CriarAsync(
            request.Tipo, request.Descricao, request.Valor,
            request.DataVencimento, request.Categoria, request.PessoaId,
            request.FormaPagamento, request.ContaBancariaId, request.TotalParcelas);
        return StatusCode(201, lancamentos);
    }

    [HttpPatch("{id}/baixar")]
    public async Task<IActionResult> Baixar(int id)
    {
        var resultado = await _financeiroService.BaixarAsync(id);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }

    [HttpPatch("{id}/cancelar")]
    public async Task<IActionResult> Cancelar(int id)
    {
        var resultado = await _financeiroService.CancelarAsync(id);
        if (!resultado.sucesso) return BadRequest(new { mensagem = resultado.mensagem });
        return Ok(new { mensagem = resultado.mensagem });
    }
}

public record LancamentoRequest(
    string Tipo, string Descricao, decimal Valor, DateTime DataVencimento,
    string? Categoria, int? PessoaId, string? FormaPagamento, int? ContaBancariaId,
    int TotalParcelas = 1);
