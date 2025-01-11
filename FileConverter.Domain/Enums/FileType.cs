namespace FileConverter.Domain.Enums
{
    public enum FileType
    {
        // Arquivos de texto
        txt,      // .txt - Arquivo de texto simples
        pdf,      // .pdf - Documento PDF
        doc,      // .doc - Documento Microsoft Word (antigo)
        docx,     // .docx - Documento Microsoft Word
        csv,      // .csv - Valores separados por vírgula
        json,     // .json - Objeto JSON
        xml,      // .xml - Documento XML
        html,     // .html ou .htm - Página HTML

        // Arquivos de imagem
        jpg,      // .jpg - Imagem JPEG
        jpeg,     // .jpeg - Imagem JPEG
        png,      // .png - Imagem PNG
        gif,      // .gif - Imagem GIF
        bmp,      // .bmp - Imagem Bitmap
        tiff,     // .tiff - Imagem TIFF
        webp,     // .webp - Imagem WebP
    }
}
