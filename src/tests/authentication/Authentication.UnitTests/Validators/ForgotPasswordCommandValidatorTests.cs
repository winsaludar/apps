namespace Authentication.UnitTests.Validators;

public class ForgotPasswordCommandValidatorTests
{
    private readonly ForgotPasswordCommandValidator _validator = new();

    [Fact]
    public async Task Email_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ForgotPasswordCommand command = new("");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ForgotPasswordCommand.Email)));
    }

    [Fact]
    public async Task Email_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        ForgotPasswordCommand command = new("email");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ForgotPasswordCommand.Email)));
    }
}
