using RequestService.Models.Db;
using RequestService.Models.Domain;
using RequestService.Models.Dtos;
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
                City = db.City,
                Street = db.Street,
                House = db.House,
                Room = db.Room,
                Flat = db.Flat,
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

    public static GetRequest MapToDto(this Request? request)
    {
        return request == null
            ? new GetRequest()
            : new GetRequest
            {
                Id = request.Id,
                City = request.City,
                Street = request.Street,
                House = request.House,
                Room = request.Room,
                Flat = request.Flat,
                Device = request.Device,
                Status = request.Status,
                Type = request.Type,
                CreationTime = request.CreationTime,
                WorkTime = request.WorkTime,
                CompletionTime = request.CompletionTime,
                GeoTag = request.GeoTag
            };
    }

    public static List<GetRequest> MapToDto(this IEnumerable<Request?>? domain)
    {
        return domain == null || !domain.Any()
            ? []
            : domain.Where(single => single != null).Select(MapToDto).ToList();
    }
}