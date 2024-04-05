namespace Blog.Application.Response
{
    public interface IResponseBase
    {
        public int StatusCode { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
        public IDictionary<string,string[]> Errors { get; set; }
    }
}
