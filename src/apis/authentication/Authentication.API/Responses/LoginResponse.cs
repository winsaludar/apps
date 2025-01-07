namespace Authentication.API.Responses;

public record LoginResponse(UserDto User, string Token);