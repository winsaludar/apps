using FluentValidation;

namespace Authentication.UnitTests.Validators;

public class ConfirmEmailCommandValidatorTests
{
    private readonly ConfirmEmailCommandValidator _validator = new();

    [Fact]
    public async Task Email_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ConfirmEmailCommand command = new("", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ConfirmEmailCommand.Email)));
    }

    [Fact]
    public async Task Email_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        ConfirmEmailCommand command = new("email", "token");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ConfirmEmailCommand.Email)));
    }

    [Fact]
    public async Task Token_IsEmpty_ReturnsAnError()
    {
        // Arrange
        ConfirmEmailCommand command = new("email@example.com", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(ConfirmEmailCommand.Token)));
    }
}
