using Microsoft.AspNetCore.Http;

namespace Blog.Domain.Core.Uploads
{
    public interface IFileHandler
    {
        public string BasePath { get; }
        public string HostUrl { get; }
        Task<UploadResult> UploadAsync(IFormFile file, string directory);
        Task<bool> DeleteFileAsync(string directory, string file);
        Task<UploadResult> UpdateImageAsync(string oldImagePath,  IFormFile newImage);
       
    }
}
