namespace Blog.Domain.Core.Uploads
{
    public interface IFileHandler
    {
        public string BasePath { get; }
        Task<UploadResult> UploadAsync(string base64File, string path);
        Task<bool> DeleteFileAsync(string path);
        Task<UploadResult> UpdateImageAsync(string oldImage,  string newImage, string path);
        bool IsAllowedMimeType(string base64string);
    }
}
