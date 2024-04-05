namespace Blog.Domain.Core.Uploads
{
    public class UploadResult
    {
        public bool IsValid { get; set; }
        public string UploadedPath { get; set; }
        public Dictionary<string, string[]> Errors { get; set; }
    }
}
