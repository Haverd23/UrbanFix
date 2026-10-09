using System;
using System.Collections.Generic;
using System.Text;
using UrbanFix.Application.Ordering;
using UrbanFix.Core.Pagination;

namespace UrbanFix.Application.DTOs
{
    public class ChamadoFiltroDTO
    {
        public string Tipo { get; set; }
        public string Bairro { get; set; }
        public string Status { get; set; }
        public int Page { get; set; }

        public int PageSize { get; set; }

        public ChamadoOrderBy? OrderBy { get; set; }

        public OrderDirection? OrderDirection { get; set; }
    }
}
