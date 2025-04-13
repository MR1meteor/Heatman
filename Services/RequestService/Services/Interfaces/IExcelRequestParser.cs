using RequestService.Models.Domain;
using Shared.DependencyInjection.Interfaces;

namespace RequestService.Services.Interfaces;

public interface IExcelRequestParser : ITransient
{
    List<ExcelRequest> GetExcelRequestsAsync(byte[] fileBytes);
}