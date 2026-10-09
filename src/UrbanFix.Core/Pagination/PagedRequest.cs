using System;
using System.Collections.Generic;
using System.Text;

namespace UrbanFix.Core.Pagination
{
    public class PagedRequest<TOrderBy>
    where TOrderBy : Enum
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public TOrderBy? OrderBy { get; set; }

        public OrderDirection? OrderDirection { get; set; }
    }
}
