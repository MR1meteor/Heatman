using ReportService.Models.Db;
using Shared.DependencyInjection.Interfaces;

namespace ReportService.DataAccess.Repositories.Interfaces;

public interface IStopResumeActRepository : ITransient
{
    Task InsertAsync(DbStopResumeAct act);
    Task<DbStopResumeAct?> GetByRequestIdAsync(Guid requestId);
}