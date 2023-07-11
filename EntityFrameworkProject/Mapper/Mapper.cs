using EntityFrameworkProject.Request;
using EntityRepository.Models;

namespace EntityFrameworkProject.Mapper;

public static class Mapper
{
    public static User RequestToDomain(this UserRequest user)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Name = user.Name
        };
    }

    public static User UpdateRequestToDomain(this UpdateUserRequest user)
    {
        return new User
        {
            Id = user.Id,
            Name = user.Name
        };
    }
}
