using ReportService.Models.Db;
using Shared.DependencyInjection.Interfaces;

namespace ReportService.DataAccess.Repositories.Interfaces;

public interface IControlActRepository : ITransient
{
    Task InsertAsync(DbControlAct act);
    Task<DbControlAct?> GetByRequestIdAsync(Guid requestId);
}