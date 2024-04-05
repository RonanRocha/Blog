using Blog.Application.Response;

namespace Blog.Application.Core.Comments.Response
{
    public class CommentCommandResponse : IResponseBase
    {
        public bool IsValid { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
