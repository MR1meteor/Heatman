namespace RequestService.Models.Dtos;

public record CreateRequestByExcel
{
    public byte[] FileBytes { get; set; } = [];
}