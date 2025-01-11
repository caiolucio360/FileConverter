using Microsoft.AspNetCore.Http;

namespace FileConverter.Application.Services.FileConverterService
{
    public interface IFileConverterService
    {
        Task<string> ConvertToBase64Async(IFormFile file);
        Task<byte[]> ConvertToPdfAsync(IFormFile file);
    }
}
