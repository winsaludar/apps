namespace Shared.Http.Responses;

public record SuccessResponse(int StatusCode, string Message) : BaseResponse(StatusCode);
