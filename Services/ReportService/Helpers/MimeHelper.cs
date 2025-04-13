namespace ReportService.Helpers;

public static class MimeHelper
{
    public static string DetectMimeType(byte[] fileData)
    {
        if (fileData.Length < 4)
            return "application/octet-stream";

        // PDF
        if (fileData.Take(4).SequenceEqual(new byte[] { 0x25, 0x50, 0x44, 0x46 }))
            return "application/pdf";

        // JPEG
        if (fileData.Take(3).SequenceEqual(new byte[] { 0xFF, 0xD8, 0xFF }))
            return "image/jpeg";

        // PNG
        if (fileData.Take(8).SequenceEqual(new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A }))
            return "image/png";

        // GIF
        if (fileData.Take(3).SequenceEqual(new byte[] { 0x47, 0x49, 0x46 }))
            return "image/gif";

        // DOCX/XLSX/PPTX (ZIP-based formats)
        if (fileData.Take(4).SequenceEqual(new byte[] { 0x50, 0x4B, 0x03, 0x04 }))
        {
            // Для Office Open XML форматов по расширению
            return "application/vnd.openxmlformats-officedocument";
        }

        // MS Office 97-2003 DOC, XLS, PPT
        if (fileData.Take(8).SequenceEqual(new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 }))
            return "application/vnd.ms-office";

        // RAR
        if (fileData.Take(7).SequenceEqual(new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07, 0x00 }))
            return "application/x-rar-compressed";

        // ZIP
        if (fileData.Take(4).SequenceEqual(new byte[] { 0x50, 0x4B, 0x03, 0x04 }))
            return "application/zip";

        return "application/octet-stream";
    }

    public static string GetFileExtension(string mimeType)
    {
        return mimeType switch
        {
            "application/pdf" => ".pdf",
            "image/jpeg" => ".jpg",
            "image/png" => ".png",
            "image/gif" => ".gif",
            "application/zip" => ".zip",
            "application/x-rar-compressed" => ".rar",
            "application/vnd.openxmlformats-officedocument" => ".docx",
            "application/vnd.ms-office" => ".doc",
            _ => ".bin"
        };
    }
}