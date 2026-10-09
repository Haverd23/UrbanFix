using UrbanFix.Application.Ordering;
using UrbanFix.Core.Pagination;

namespace UrbanFix.Api.Request
{
    public class ListarChamadosRequest : PagedRequest<ChamadoOrderBy>
    {
        public string? Tipo { get; set; }
        public string? Bairro { get; set; }
        public string? Status { get; set; }
    }
}
