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
    private readonly IMediator _mediator;
    private readonly ILogger<UserController> _logger;

    public UserController(
        ILogger<UserController> logger,
        IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpGet]
    [Route("/users/get")]
    [ProducesResponseType(typeof(List<User>), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get()
    {
        var query = new GetUsersQuery();
        var users = await _mediator.Send(query);

        return users is not null ? Ok(users) : NotFound("Users not found");
    }

    [HttpGet]
    [Route("/users/{id:guid}")]
    [ProducesResponseType(typeof(User), 200)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Get(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var user = await _mediator.Send(query);

        return user is not null ? Ok(user) : NotFound("User not found");
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
            var user = await  _mediator.Send(command);
            _logger.LogInformation($"User {user.Name} was created");

            return Ok(user);
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
            var user = requestUser.UpdateRequestToDomain();
            var command = new UpdateUserCommand(user);
            User? responseUser = await _mediator.Send(command);

            if (responseUser is not null)
            {
                _logger.LogInformation($"The user {user.Name} was updated to {responseUser.Name}");

                return Ok(responseUser);
            }

            return BadRequest();
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
            var command = new DeleteUserCommand(id);
            var user = await _mediator.Send(command);

            if (user is not null)
            {
                _logger.LogInformation("The user was sucessfully deleted");

                return Ok(user);
            }

            return NotFound("User not found");
        }
        catch (Exception ex)
        {
            _logger.LogError("There was an issue for deleting the user", ex);
            return StatusCode(500, ex);
        }
    }
}
