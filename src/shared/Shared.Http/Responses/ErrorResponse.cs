namespace Shared.Http.Responses;

public record ErrorResponse(int StatusCode, string Error, string[] Details) : BaseResponse(StatusCode);
