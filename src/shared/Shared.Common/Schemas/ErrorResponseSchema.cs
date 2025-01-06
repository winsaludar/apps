namespace Shared.Common.Schemas;

public record ErrorResponseSchema(int StatusCode, string Error, string[] Details);
