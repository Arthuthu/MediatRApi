using EntityRepository.Models;
using EntityRepository.Repository;
using MediatR;

namespace EntityFrameworkProject.MediatR.UserCommands;

public record DeleteUserCommand(Guid Id) : IRequest<User?>;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, User?>
{
    private readonly IUserRepository _userRepository;

    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.Delete(request.Id);

        return user is not null ? user : null;
    }
}
