namespace Authentication.UnitTests.Validators;

public class ResetPasswordCommandValidatorTests
{
    private readonly ResetPasswordCommandValidator _validator = new();

    [Fact]
    public async Task Email_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ResetPasswordCommand command = new("", "password", "password", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ResetPasswordCommand.Email)));
    }

    [Fact]
    public async Task Email_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        ResetPasswordCommand command = new("email", "password", "password", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ResetPasswordCommand.Email)));
    }

    [Fact]
    public async Task Password_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "", "password", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ResetPasswordCommand.Password)));
    }

    [Fact]
    public async Task Password_IsShortInLength_ReturnsAnError()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "p", "password", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ResetPasswordCommand.Password)));
    }

    [Fact]
    public async Task RetypePassword_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "password", "", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ResetPasswordCommand.RetypePassword)));
    }

    [Fact]
    public async Task RetypePassword_DidNotMatchPassword_ReturnsAnError()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "password", "different-password", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ResetPasswordCommand.RetypePassword)));
    }
}
