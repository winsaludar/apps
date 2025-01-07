namespace Shared.Common.Responses;

public record ErrorResponse(int StatusCode, string Error, string[] Details);
