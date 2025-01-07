namespace Authentication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterUserCommand command, CancellationToken cancellationToken)
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
        
        return Ok(new LoginResponse(
            new UserDto(user.Id, user.Username, user.Email),
            new TokenDto(token.Value, token.RefreshToken, token.ExpiresAt)));
    }

    [HttpPost("refresh-token")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LoginResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> RefreshTokenAsync([FromBody] RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        (User user, Token token) = await mediator.Send(command, cancellationToken);

        return Ok(new LoginResponse(
            new UserDto(user.Id, user.Username, user.Email),
            new TokenDto(token.Value, token.RefreshToken, token.ExpiresAt)));
    }

    [HttpPost("resend-email-confirmation")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ResendEmailConfirmationAsync([FromBody] ResendEmailConfirmationCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok(new SuccessResponse(200, "Please check your inbox/spam folder"));
    }

    [HttpPost("confirm-email")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ConfirmEmailAsync([FromBody] ConfirmEmailCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok(new SuccessResponse(200, "Your email is now verified"));
    }

    [HttpPost("forgot-password")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SuccessResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return Ok(new SuccessResponse(200, "Please check your inbox/spam folder"));
    }
}
