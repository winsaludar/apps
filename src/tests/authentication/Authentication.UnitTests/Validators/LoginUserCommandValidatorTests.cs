namespace Authentication.UnitTests.Validators;

public class LoginUserCommandValidatorTests
{
    public readonly LoginUserCommandValidator _validator = new();

    [Fact]
    public async Task Email_IsEmpty_ReturnsAnError()
    {
        // Arrange
        LoginUserCommand command = new("", "password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(LoginUserCommand.Email)));
    }

    [Fact]
    public async Task Email_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        LoginUserCommand command = new("email", "password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(LoginUserCommand.Email)));
    }

    [Fact]
    public async Task Password_IsEmpty_ReturnsAnError()
    {
        // Arrange
        LoginUserCommand command = new("email", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(LoginUserCommand.Password)));
    }
}
