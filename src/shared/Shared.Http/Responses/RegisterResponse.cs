namespace Shared.Http.Responses;

public record RegisterResponse(Guid Id, string Username, string Email) : BaseResponse((int) HttpStatusCode.OK);
