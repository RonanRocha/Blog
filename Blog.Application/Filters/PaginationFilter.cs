namespace Blog.Application.Filters
{
    public class PaginationFilter
    {
        public int PageNumber { get;  set; }
        public int PageSize { get;  set; }

        public PaginationFilter()
        {
            
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber < 1 ? 1 : pageNumber;
            this.PageSize = pageSize > 100 ? 100 : pageSize;
        }
    }
}
