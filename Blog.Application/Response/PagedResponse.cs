namespace Blog.Application.Response
{

        public class PagedResponse<T> : IResponseBase 
        {

            public int PageNumber { get; set; }
            public int PageSize { get; set; }
            public Uri FirstPage { get; set; }
            public Uri LastPage { get; set; }
            public int TotalPages { get; set; }
            public int TotalRecords { get; set; }
            public Uri NextPage { get; set; }
            public Uri PreviousPage { get; set; }
            public int StatusCode { get; set; }
            public bool IsValid { get; set ; }
            public string Message { get; set ; }
            public T Data { get; set; }

            public IDictionary<string, string[]> Errors { get ; set ; }

            public PagedResponse()
            {
                
            }
            public PagedResponse(T data, int pageNumber, int pageSize)
            {
                this.PageNumber = pageNumber;
                this.PageSize = pageSize;
                this.Message = null;
                this.Errors = null;
                this.Data = data;
            }
        }
    
}
