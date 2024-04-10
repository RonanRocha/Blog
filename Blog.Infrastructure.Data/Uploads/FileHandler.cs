using Blog.Domain.Core.Uploads;
using Microsoft.AspNetCore.Http;

namespace Blog.Infrastructure.Data.Uploads
{
    public class FileHandler : IFileHandler
    {

        public string BasePath { get;  private set; } = "D:/projetos/Blog/Blog.Api/wwwroot/";
        public string HostUrl { get; set; } = "https://localhost:7093/";

        private string[] permittedExtensions = { ".jpg", ".jpeg", ".png" };

        public async Task<UploadResult> UploadAsync(IFormFile file, string directory)
        {
            try
            {
                

                if (!permittedExtensions.Contains(Path.GetExtension(file.FileName)) || file.Length > (4 * 1024 * 1024))
                {
                    var dictonaryErrors = new Dictionary<string, string[]>
                    {
                        { "validation", new string[] { "Format invalid" } }
                    };
                    return new UploadResult
                    {
                        IsValid = false,
                        Errors = dictonaryErrors
                    };
                };


                string fileName = Guid.NewGuid().ToString() + ".jpg";

                string uploadPath = Path.Combine(BasePath, directory);

                if (!Directory.Exists(uploadPath)) 
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, fileName);

                using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                await file.CopyToAsync(fs);
              

                string publicDirectory =  Path.Combine(HostUrl,"Uploads/Posts/");

                return new UploadResult
                {
                    IsValid = true,
                    UploadedPath = Path.Combine(publicDirectory, fileName),
                };
            }
            catch (Exception ex)
            {
                return new UploadResult
                {
                    IsValid = false,
                };
            }
        }
  
        public Task<bool> DeleteFileAsync(string directory, string file)
        {
            var path = new string[]
            {
                BasePath,
                directory,
                file
            };

            string filePath = Path.Combine(path);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
        public async Task<UploadResult> UpdateImageAsync(string oldImage, IFormFile newImage)
        {
            var filePath = Path.GetFileName(oldImage);
            bool isDeleted = await DeleteFileAsync("Uploads/Posts/", filePath);

            if (isDeleted)
                return  await UploadAsync(newImage, "Uploads/Posts/");

            return new UploadResult { IsValid = false };
        }
    }
}
