namespace Shared.Http.Responses;

public record LoginResponse(UserDto User, TokenDto Token) : BaseResponse((int)HttpStatusCode.OK);
