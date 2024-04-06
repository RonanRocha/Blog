namespace Blog.Domain.Core.Uploads
{
    public interface IFileHandler
    {
        public string BasePath { get; }
        public string HostUrl { get; }
        Task<UploadResult> UploadAsync(string base64File, string directory);
        Task<bool> DeleteFileAsync(string directory, string file);
        Task<UploadResult> UpdateImageAsync(string oldImage,  string newImage);
        bool IsAllowedMimeType(string base64string);
    }
}
