using Microsoft.AspNetCore.Http;

namespace FileConverter.Application.Services.FileConverterService
{
    public class FileConverterService : IFileConverterService
    {
        public async Task<string> ConvertToBase64Async(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return Convert.ToBase64String(memoryStream.ToArray());
        }
    }
}
