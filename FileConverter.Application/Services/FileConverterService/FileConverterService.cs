using FileConverter.Application.Services.FileConverterService.Models;
using FluentValidation;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
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

        public async Task<byte[]> ConvertToPdfAsync(IFormFile file)
        {
            var fileType = file.ContentType.ToLower();

            using var memoryStream = new MemoryStream();
            using var pdfWriter = new PdfWriter(memoryStream);
            using var pdfDocument = new PdfDocument(pdfWriter);
            var document = new Document(pdfDocument);

            if (fileType.StartsWith("text"))
            {
                using var reader = new StreamReader(file.OpenReadStream());
                var content = await reader.ReadToEndAsync();
                document.Add(new Paragraph(content));
            }

            document.Close();

            return memoryStream.ToArray();
        }
    }
}
