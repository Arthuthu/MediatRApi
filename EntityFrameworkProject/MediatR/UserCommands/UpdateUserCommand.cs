using EntityRepository.Models;
using EntityRepository.Repository;
using MediatR;

namespace EntityFrameworkProject.MediatR.UserCommands;

public record UpdateUserCommand(User User) : IRequest<User?>;

public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, User?>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.Update(request.User);

        return user is not null ? user : null;
    }
}
