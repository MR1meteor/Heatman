using RequestService.Models.Db;
using RequestService.Models.Domain;
using RequestService.Models.Enums;

namespace RequestService.Mapping;

public static class RequestMapper
{
    public static Request? MapToDomain(this DbRequest? db)
    {
        return db == null
            ? null
            : new Request
            {
                Id = db.Id,
                Address = db.Address,
                Device = db.Device,
                Status = (RequestStatus)db.Status,
                Type = (RequestType)db.Type,
                CreationTime = db.CreationTime,
                WorkTime = db.WorkTime,
                CompletionTime = db.CompletionTime,
                BrigadeId = db.BrigadeId
            };
    }

    public static List<Request> MapToDomain(this IEnumerable<DbRequest?>? db)
    {
       return db == null || !db.Any()
            ? []
            : db.Where(single => single != null).Select(MapToDomain).ToList();
    }
}