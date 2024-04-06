using Blog.Domain.Core.Uploads;

namespace Blog.Infrastructure.Data.Uploads
{
    public class FileHandler : IFileHandler
    {

        public string BasePath { get;  private set; } = "D:/projetos/Blog/Blog.Api/wwwroot/";
        public string HostUrl { get; set; } = "https://localhost:7093/";

        public async Task<UploadResult> UploadAsync(string file, string directory)
        {
            try
            {      
                int fileSize = FileSize(file);

                if (!IsAllowedMimeType(file) || fileSize > (4 * 1024 * 1024))
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

                var bytes = Convert.FromBase64String(file);

                string fileName = Guid.NewGuid().ToString() + ".jpg";

                string uploadPath = Path.Combine(BasePath, directory);

                if (!Directory.Exists(uploadPath)) 
                {
                    Directory.CreateDirectory(uploadPath);
                }

                string filePath = Path.Combine(uploadPath, fileName);

                using FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write);

                await fs.WriteAsync(bytes, 0, bytes.Length);

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
        public bool IsAllowedMimeType(string base64string)
        {
            if (string.IsNullOrWhiteSpace(base64string))
            {
                return false;
            }

            string data = base64string.Substring(0, 5);

            switch (data.ToUpper())
            {
                case "IVBOR":
                    //png
                    return true;
                    break;
                case "/9J/4":
                    //jpg
                    return true;
                    break;
                default:
                    //other types
                    return false;
            }
        }
        public int FileSize(string base64String)
        {
            var base64 = base64String.Replace("=", "");
            return Convert.ToInt32(base64.Length * (3 / 4));
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
        public async Task<UploadResult> UpdateImageAsync(string oldImage, string newImage)
        {
            var filePath = Path.GetFileName(oldImage);
            bool isDeleted = await DeleteFileAsync("Uploads/Posts/", filePath);

            if (isDeleted)
                return  await UploadAsync(newImage, "Uploads/Posts/");

            return new UploadResult { IsValid = false };
        }
    }
}
