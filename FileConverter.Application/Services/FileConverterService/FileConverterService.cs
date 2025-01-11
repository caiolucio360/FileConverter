using FileConverter.Domain.Enums;
using Microsoft.AspNetCore.Http;
using PdfSharpCore.Drawing;
using PdfSharpCore.Drawing.Layout;
using PdfSharpCore.Pdf;

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

        public byte[] ConvertToPdf(IFormFile file)
        {
            var fileType = Path.GetExtension(file.FileName)
                ?? throw new NotSupportedException("Tipo de arquivo não suportado.");

            fileType = fileType[1..];


            _ = Enum.TryParse(fileType, out FileType fileTypeEnum);

            if (FileTypeExtensions.IsImage(fileTypeEnum))
                return ConvertImageToPdf(file);
            else if (FileTypeExtensions.IsText(fileTypeEnum))
                return ConvertTextToPdf(file);
            else
                throw new NotSupportedException("Tipo de arquivo não suportado.");
        }

        public static byte[] ConvertImageToPdf(IFormFile imageFile)
        {
            var document = new PdfDocument();
            var page = document.AddPage();

            var gfx = XGraphics.FromPdfPage(page);

            using (var imageStream = imageFile.OpenReadStream())
            {
                var image = XImage.FromStream(() => imageStream);

                page.Width = image.PixelWidth;
                page.Height = image.PixelHeight;

                gfx.DrawImage(image, 0, 0, image.PixelWidth, image.PixelHeight);
            }

            using var outputStream = new MemoryStream();
            document.Save(outputStream);
            return outputStream.ToArray();
        }

        public static byte[] ConvertTextToPdf(IFormFile file)
        {
            var title = Path.GetFileNameWithoutExtension(file.FileName);

            string content;
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                content = reader.ReadToEnd();
            }

            var pdfDocument = new PdfDocument();
            var page = pdfDocument.AddPage();

            var gfx = XGraphics.FromPdfPage(page);
            var titleFont = new XFont("Arial", 20, XFontStyle.Bold);
            var contentFont = new XFont("Arial", 12, XFontStyle.Regular);

            gfx.DrawString(title, titleFont, XBrushes.Black,
                new XRect(0, 0, page.Width, 50), XStringFormats.Center);

            var layoutRect = new XRect(50, 100, page.Width - 100, page.Height - 150);
            var textFormatter = new XTextFormatter(gfx)
            {
                Alignment = XParagraphAlignment.Left
            };

            textFormatter.DrawString(content, contentFont, XBrushes.Black, layoutRect);

            using var stream = new MemoryStream();
            pdfDocument.Save(stream);
            return stream.ToArray();
        }
    }

    public static class FileTypeExtensions
    {
        public static bool IsImage(this FileType fileType) =>
            fileType is FileType.jpg or FileType.png or FileType.gif or FileType.bmp or FileType.tiff or FileType.webp or FileType.jpeg;

        public static bool IsText(this FileType fileType) =>
            fileType is FileType.txt or FileType.pdf or FileType.doc or FileType.docx or FileType.csv or FileType.json or FileType.xml or FileType.html;
    }
}
