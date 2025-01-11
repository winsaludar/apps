namespace Shared.Http.Dtos;

public record TokenDto(string Value, string RefreshToken, DateTime ExpiresAt);