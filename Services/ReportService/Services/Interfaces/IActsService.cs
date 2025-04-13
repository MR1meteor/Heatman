using ReportService.Models.Dtos;
using Shared.DependencyInjection.Interfaces;

namespace ReportService.Services.Interfaces;

public interface IActsService : ITransient
{
    Task<bool> CreateControlActAsync(CreateControlActRequest createControlActRequest);
    Task<bool> CreateStopResumeActAsync(CreateStopResumeActRequest createStopResumeActRequest);
    Task<byte[]> GetControlActAsync(Guid requestId);
}