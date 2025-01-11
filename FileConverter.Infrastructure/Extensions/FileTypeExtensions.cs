using FileConverter.Domain.Enums;

namespace FileConverter.Infrastructure.Extensions
{
    public static class FileTypeExtensions
    {
        public static bool IsImage(this FileType fileType) =>
            fileType is FileType.jpg or FileType.png or FileType.gif or FileType.bmp or FileType.tiff or FileType.webp;

        public static bool IsText(this FileType fileType) =>
            fileType is FileType.txt or FileType.pdf or FileType.doc or FileType.docx or FileType.csv or FileType.json or FileType.xml or FileType.html;
    }
}
