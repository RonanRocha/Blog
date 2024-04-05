using Blog.Application.Response;

namespace Blog.Application.Core.Categories.Response
{
    public class CategoryCommandResponse : IResponseBase
    {
        public int StatusCode { get; set; }
        public bool IsValid { get; set ; }
        public string Message { get; set ; }
        public IDictionary<string, string[]> Errors { get; set ; }
    }
}
