using EntityRepository.Models;
using EntityRepository.Repository;
using MediatR;

namespace EntityFrameworkProject.MediatR.UserCommands;

public record GetUsersQuery() : IRequest<List<User>?>;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, List<User>?>
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>?> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.Get(cancellationToken);

        return users;
    }
}

