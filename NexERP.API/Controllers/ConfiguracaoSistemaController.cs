using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.Services;
using NexERP.Domain.Entities;

namespace NexERP.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ConfiguracaoSistemaController : ControllerBase
{
    private readonly ConfiguracaoSistemaService _service;

    public ConfiguracaoSistemaController(ConfiguracaoSistemaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Obter()
    {
        var config = await _service.ObterAsync();
        return Ok(config ?? new ConfiguracaoSistema());
    }

    [HttpPost]
    public async Task<IActionResult> Salvar([FromBody] ConfiguracaoSistema request)
    {
        await _service.SalvarAsync(request);
        return Ok(new { mensagem = "Configurações salvas com sucesso." });
    }
}