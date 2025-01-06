namespace Authentication.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IMediator mediator) : ControllerBase
{
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Guid))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RegisterAsync([FromBody]RegisterUserCommand command)
    {
        User newUser = await mediator.Send(command);

        return Ok(new 
        {
            id = newUser.Id,
            username = newUser.Username,
            email = newUser.Email
        });
    }
}
