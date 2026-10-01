using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanFix.Api.Request;
using UrbanFix.Application.Commands.CriarChamado;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Queries.ListarChamados;

namespace UrbanFix.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        private readonly CriarChamadoCommandHandler _handler;
        private readonly ListarChamadosQueryHandler _queryHandler;

        public ChamadosController(CriarChamadoCommandHandler handler, ListarChamadosQueryHandler queryHandler)
        {
            _handler = handler;
            _queryHandler = queryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CriarChamado(CriarChamadoRequest request)
        {
            var chamado = new CriarChamadoCommand(request.Tipo, request.Descricao, request.CEP, request.Numero);
            var resultado = await _handler.HandleAsync(chamado);

            return Created("",resultado);
        }
        [HttpGet]
        public async Task<IActionResult> ListarChamados()
        {
            return Ok( await _queryHandler.HandleAsync());
        }
    }
}
