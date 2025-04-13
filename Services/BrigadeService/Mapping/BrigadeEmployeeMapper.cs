using BrigadeService.Models.Db;
using BrigadeService.Models.Domain;

namespace BrigadeService.Mapping;

public static class BrigadeEmployeeMapper
{
    public static DbBrigadeEmployee? MapToDb(this BrigadeEmployee? domain)
    {
        return domain == null
            ? null
            : new DbBrigadeEmployee
            {
                BrigadeId = domain.BrigadeId,
                EmployeeId = domain.EmployeeId
            };
    }

    public static List<DbBrigadeEmployee> MapToDb(this IEnumerable<BrigadeEmployee?>? domain)
    {
        return domain == null || !domain.Any()
            ? []
            : domain.Where(single => single != null).Select(MapToDb).ToList();
    }
}