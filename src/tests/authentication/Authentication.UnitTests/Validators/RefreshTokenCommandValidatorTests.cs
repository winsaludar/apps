namespace Authentication.UnitTests.Validators;

public class RefreshTokenCommandValidatorTests
{
    private readonly RefreshTokenCommandValidator _validator = new();

    [Fact]
    public async Task Token_IsEmpty_ReturnsAnError()
    {
        // Arrange
        RefreshTokenCommand command = new("", "refresh-token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RefreshTokenCommand.Token)));
    }

    [Fact]
    public async Task RefreshToken_IsEmpty_ReturnsAnError()
    {
        // Arrange
        RefreshTokenCommand command = new("token", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RefreshTokenCommand.RefreshToken)));
    }
}
