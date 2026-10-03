using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanFix.Api.Request;
using UrbanFix.Application.Commands.CriarChamado;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Queries.ListarChamados;
using UrbanFix.Core.Mediator;
using UrbanFix.Core.Web.ApiResponse;

namespace UrbanFix.Api.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ChamadosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChamadosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarChamado(CriarChamadoRequest request)
        {
            var chamado = new CriarChamadoCommand(request.Tipo, request.Descricao, request.CEP, request.Numero);
            var resultado = await _mediator.SendAsync<CriarChamadoCommand, Guid>(chamado);

            return Created("",ApiResponse<Guid>.OK(resultado));
        }
        [HttpGet]
        public async Task<IActionResult> ListarChamados()
        {
            var query = new ListarChamadosQuery();
            var dados = await _mediator.SendAsync<ListarChamadosQuery,IEnumerable<ChamadoDTO>>(query);
            return Ok(ApiResponse<IEnumerable<ChamadoDTO>>.OK(dados));
        }
    }
}
