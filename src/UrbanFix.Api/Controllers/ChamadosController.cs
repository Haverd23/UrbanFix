using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UrbanFix.Api.Request;
using UrbanFix.Application.Commands.CriarChamado;
using UrbanFix.Application.Commands.FinalizarChamado;
using UrbanFix.Application.DTOs;
using UrbanFix.Application.Queries.ListarChamados;
using UrbanFix.Core.Mediator;
using UrbanFix.Core.Pagination;
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
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> FinalizarChamado([FromRoute] FinalizarChamadoRequest request)
        {
            var command = new FinalizarChamadoCommand(request.Id);

            var resultado = await _mediator
                .SendAsync<FinalizarChamadoCommand, Guid>(command);

            return Ok(ApiResponse<Guid>.OK(resultado));
        }
        [HttpGet]
        public async Task<IActionResult> ListarChamados([FromQuery]ListarChamadosRequest request)
        {
            var filtro = new ChamadoFiltroDTO
            {
                Tipo = request.Tipo,
                Bairro = request.Bairro,
                Status = request.Status,
                OrderBy = request.OrderBy,
                OrderDirection = request.OrderDirection,
                Page = request.Page,
                PageSize = request.PageSize
            };
            var query = new ListarChamadosQuery(filtro);
            var dados = await _mediator.SendAsync<ListarChamadosQuery,PagedResult<ChamadoDTO>>(query);
            return Ok(ApiResponse<PagedResult<ChamadoDTO>>.OK(dados));
        }
    }
}
