using AuthService.Models.Db;
using AuthService.Models.Domain;

namespace AuthService.Mapping;

public static class UserMapper
{
    public static DbUser? MapToDb(this User? domain)
    {
        return domain == null
            ? null
            : new DbUser
            {
                Id = domain.Id,
                VerificationCode = domain.VerificationCode
            };
    }

    public static User? MapToDomain(this DbUser? db)
    {
        return db == null
            ? null
            : new User
            {
                Id = db.Id,
                VerificationCode = db.VerificationCode
            };
    }
}