using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Services.Interfaces;
using UrbanFix.Domain.Models;
using UrbanFix.WebApi.Services;

namespace UrbanFix.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChamadoController : ControllerBase
    {
        private readonly IChamadoAppService _chamadoAppService;

        public ChamadoController(IChamadoAppService chamadoAppService)
        {
            _chamadoAppService = chamadoAppService;
        }

        // GET /api/chamado/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorIdAsync(Guid id)
        {
            var resultado = await _chamadoAppService.ObterPorIdAsync(id);
            if (resultado == null)
                return NotFound("Chamado não encontrado");

            return Ok(resultado);
        }

        // POST /api/chamado
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AdicionarAsync([FromForm] CriarChamadoDTO criarChamadoDTO, [FromForm] UploadImagem imagem)
        {
            try
            {
                if (imagem?.Imagem != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagem.Imagem.CopyToAsync(memoryStream);
                        var imagemBytes = memoryStream.ToArray();
                        var base64Imagem = Convert.ToBase64String(imagemBytes); 
                        criarChamadoDTO.Base64Imagem = base64Imagem; 
                    }
                }

                await _chamadoAppService.AdicionarAsync(criarChamadoDTO);
                return StatusCode(StatusCodes.Status201Created, criarChamadoDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT /api/chamado/{id}/status
        [HttpPut("{id}/status")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AtualizarStatusAsync(Guid id, [FromBody] Chamado.TipoDeStatus novoStatus)
        {
            try
            {
                await _chamadoAppService.AtualizarStatusAsync(id, novoStatus);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE /api/chamado/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoverAsync(Guid id)
        {
            try
            {
                await _chamadoAppService.RemoverAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/chamado
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodosAsync()
        {
            var resultado = await _chamadoAppService.ObterTodosAsync();
            return Ok(resultado);
        }

        // GET /api/chamado/status/{status}
        [HttpGet("status/{status}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorStatusAsync(Chamado.TipoDeStatus status)
        {
            var resultado = await _chamadoAppService.ObterPorStatusAsync(status);
            return Ok(resultado);
        }

        // GET /api/chamado/tipo/{tipo}
        [HttpGet("tipo/{tipo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorTipoAsync(Chamado.TipoDeProblema tipo)
        {
            var resultado = await _chamadoAppService.ObterPorTipoAsync(tipo);
            return Ok(resultado);
        }

        // GET /api/chamado/status/{status}/antigos
        [HttpGet("status/{status}/antigos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorStatusMaisAntigosAsync(Chamado.TipoDeStatus status)
        {
            var resultado = await _chamadoAppService.ObterPorStatusMaisAntigosAsync(status);
            return Ok(resultado);
        }

        // GET /api/chamado/tipo/{tipo}/antigos
        [HttpGet("tipo/{tipo}/antigos")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObterPorTipoMaisAntigosAsync(Chamado.TipoDeProblema tipo)
        {
            var resultado = await _chamadoAppService.ObterPorTipoMaisAntigosAsync(tipo);
            return Ok(resultado);
        }
    }
}