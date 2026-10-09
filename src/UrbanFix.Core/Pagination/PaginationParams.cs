namespace UrbanFix.Core.Pagination
{
    public class PaginationParams
    {
        public int Page { get; }
        public int PageSize { get; }
        public int Skip => (Page - 1) * PageSize;

        public PaginationParams(int page, int pageSize, int maxPageSize = 50)
        {
            Page = page <= 0 ? 1 : page;
            PageSize = pageSize <= 0 ? 10 : pageSize;
            if (PageSize > maxPageSize) PageSize = maxPageSize;
        }
           
    }
}
