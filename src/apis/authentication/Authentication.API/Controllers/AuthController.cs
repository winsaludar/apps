namespace Authentication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserCommand command, CancellationToken cancellationToken)
    {
        User newUser = await mediator.Send(command, cancellationToken);
        return Ok(new RegisterResponse(newUser.Id, newUser.Username, newUser.Email));
    }

    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand command, CancellationToken cancellationToken)
    {
        (User user, Token token) = await mediator.Send(command, cancellationToken);
        return Ok(new LoginResponse(new UserDto(user.Id, user.Username, user.Email), token.Value));
    }
}
