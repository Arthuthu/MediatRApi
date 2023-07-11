using EntityRepository.Models;
using EntityRepository.Repository;
using MediatR;

namespace EntityFrameworkProject.MediatR.UserCommands;

public record CreateUserCommand(string Name) : IRequest<User>;

public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IUserRepository _userRepository;

    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        User user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };

        await _userRepository.Create(user);
        return user;
    }
}
