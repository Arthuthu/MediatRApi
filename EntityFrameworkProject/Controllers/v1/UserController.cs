using EntityFrameworkProject.Mapper;
using EntityFrameworkProject.MediatR.UserCommands;
using EntityFrameworkProject.Request;
using EntityRepository;
using EntityRepository.Models;
using EntityRepository.Repository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkProject.Controllers.v1;

[Route("api/v1/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(
        IUserRepository userRepository,
        ILogger<UserController> logger,
        IMediator mediator)
    {
        _userRepository = userRepository;
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/users/get")]
    [ProducesResponseType(typeof(List<User>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get()
    {
        List<User>? users = await _userRepository.Get();

        return users is not null ? Ok(users) : NotFound();
    }

    [HttpGet]
    [Route("/users/{id:guid}")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetUserQuery(id);
        var userResult = await _mediator.Send(query);

        return userResult is not null ? Ok(userResult) : NotFound();
    }

    [HttpPost]
    [Route("/user/post")]
    [ProducesResponseType(typeof(User),200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Post(UserRequest requestUser)
    {
        try
        {
            var command = new CreateUserCommand(requestUser.Name);

            var userResult = await  _mediator.Send(command);
            _logger.LogInformation($"User {userResult.Name} was created");

            return Ok(userResult);
        }
        catch (Exception ex)
        {
            _logger.LogError($"There was an issue with creating a user", ex);
            return StatusCode(500, ex);
        }
    }

    [HttpPut]
    [Route("/users/update")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Put(UpdateUserRequest requestUser)
    {
        try
        {
            User user = requestUser.UpdateRequestToDomain();
            bool userWasUpdated = await _userRepository.Update(user);
            _logger.LogInformation($"The user {user.Name} was updated.");

            return userWasUpdated is true ? Ok(user) : BadRequest();
        }
        catch (Exception ex)
        {
            _logger.LogError("There was an issue for updating the user", ex);
            return StatusCode(500, ex);
        }
    }

    [HttpDelete]
    [Route("/users/delete")]
    [ProducesResponseType( 200)]
    [ProducesResponseType(404)]
    [ProducesResponseType(500)]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            bool userWasDeleted = await _userRepository.Delete(id);
            _logger.LogInformation("The user was sucessfully deleted");

            return userWasDeleted is true ? Ok() : NotFound();
        }
        catch (Exception ex)
        {
            _logger.LogError("There was an issue for deleting the user", ex);
            return StatusCode(500, ex);
        }
    }
}
