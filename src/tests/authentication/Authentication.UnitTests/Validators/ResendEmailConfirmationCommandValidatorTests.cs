namespace Authentication.UnitTests.Validators;

public class ResendEmailConfirmationCommandValidatorTests
{
    private readonly ResendEmailConfirmationCommandValidator _validator = new();

    [Fact]
    public async Task Email_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ResendEmailConfirmationCommand command = new("");

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
        ResendEmailConfirmationCommand command = new("email");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(LoginUserCommand.Email)));
    }
}
