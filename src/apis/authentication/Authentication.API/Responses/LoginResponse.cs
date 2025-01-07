namespace Authentication.API.Responses;

public record LoginResponse(UserDto User, TokenDto Token);