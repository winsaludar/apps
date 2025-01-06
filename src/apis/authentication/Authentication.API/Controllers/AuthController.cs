namespace Authentication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RegisterResponse))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponseSchema))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorResponseSchema))]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserCommand command)
    {
        User newUser = await mediator.Send(command);
        return Ok(new RegisterResponse(newUser.Id, newUser.Username, newUser.Email));
    }
}
