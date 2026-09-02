using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NexERP.Application.DTOs;
using NexERP.Application.Services;

namespace NexERP.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly ClienteService _clienteService;

        public ClientesController(ClienteService clienteService)
        {
            _clienteService = clienteService ?? throw new ArgumentNullException(nameof(clienteService));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodos()
        {
            try
            {
                var clientes = await _clienteService.ObterTodosAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpGet("ativos")]
        public async Task<IActionResult> ObterAtivos()
        {
            try
            {
                var clientes = await _clienteService.ObterAtivosAsync();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var cliente = await _clienteService.ObterPorIdAsync(id);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpGet("cnpjcpf/{cnpjCpf}")]
        public async Task<IActionResult> ObterPorCnpjCpf(string cnpjCpf)
        {
            try
            {
                var cliente = await _clienteService.ObterPorCnpjCpfAsync(cnpjCpf);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorRazaoSocial([FromQuery] string razaoSocial)
        {
            try
            {
                var clientes = await _clienteService.BuscarPorRazaoSocialAsync(razaoSocial);
                return Ok(clientes);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpGet("total-ativos")]
        public async Task<IActionResult> TotalAtivos()
        {
            try
            {
                var total = await _clienteService.TotalClientesAtivosAsync();
                return Ok(new { total });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarClienteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = await _clienteService.CriarAsync(dto);
                return CreatedAtAction(nameof(ObterPorId), new { id = cliente.Id }, cliente);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Atualizar(Guid id, [FromBody] AtualizarClienteDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var cliente = await _clienteService.AtualizarAsync(id, dto);
                return Ok(cliente);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpPatch("{id:guid}/limite-credito")]
        public async Task<IActionResult> AtualizarLimiteCredito(Guid id, [FromBody] decimal novoLimite)
        {
            try
            {
                await _clienteService.AtualizarLimiteCreditoAsync(id, novoLimite);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpPatch("{id:guid}/desativar")]
        public async Task<IActionResult> Desativar(Guid id)
        {
            try
            {
                await _clienteService.DesativarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpPatch("{id:guid}/ativar")]
        public async Task<IActionResult> Ativar(Guid id)
        {
            try
            {
                await _clienteService.AtivarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensagem = "Erro interno.", detalhe = ex.Message });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Remover(Guid id)
        {
            try
            {
                await _clienteService.RemoverAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { mensagem = ex.Message });
            }
            catch (Exception ex)
            {