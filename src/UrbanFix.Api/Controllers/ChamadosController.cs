using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanFix.Api.Request;
using UrbanFix.Application.Commands.CriarChamado;

namespace UrbanFix.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        private readonly CriarChamadoCommandHandler _handler;

        public ChamadosController(CriarChamadoCommandHandler handler)
        {
            _handler = handler;
        }

        [HttpPost]
        public async Task<IActionResult> CriarChamado(CriarChamadoRequest request)
        {
            var chamado = new CriarChamadoCommand(request.Tipo, request.Descricao, request.CEP, request.Numero);
            var resultado = await _handler.HandleAsync(chamado);

            return Created("",resultado);
        }
    }
}
