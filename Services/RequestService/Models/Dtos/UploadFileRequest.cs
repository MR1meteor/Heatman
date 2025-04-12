namespace RequestService.Models.Dtos;

public record UploadFileRequest
{
    public byte[] FileBytes { get; set; } = [];
}