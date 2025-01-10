using Microsoft.AspNetCore.Http;

namespace FileConverter.Application.Services.FileConverterService.Models
{
    public class FileUploadModel
    {
        public required IFormFile File { get; set; }
    }
}
