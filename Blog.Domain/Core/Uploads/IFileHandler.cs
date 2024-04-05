namespace Blog.Domain.Core.Uploads
{
    public interface IFileHandler
    {
        Task<UploadResult> UploadAsync(string base64File, string path);
        Task<bool> DeleteFileAsync(string path);
        bool IsAllowedMimeType(string base64string);
    }
}
