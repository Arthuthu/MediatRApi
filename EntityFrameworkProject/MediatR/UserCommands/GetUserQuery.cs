using EntityRepository.Models;
using EntityRepository.Repository;
using MediatR;

namespace EntityFrameworkProject.MediatR.UserCommands;

public record GetUserQuery(Guid Id) : IRequest<User?>;

public class GetUserHandler : IRequestHandler<GetUserQuery, User?>
{
    private readonly IUserRepository _userRepository;

    public GetUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Get(request.Id);

        return user is not null ? user : null;
    }
}
