namespace Authentication.UnitTests.Validators;

public class RegisterUserCommandValidatorTests
{
    private readonly RegisterUserCommandValidator _validator = new();

    [Fact]
    public async Task Username_IsEmpty_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("", "email@email.com", "password", "password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.Username)));
    }

    [Fact]
    public async Task Email_IsEmpty_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("username", "", "password", "password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.Email)));
    }

    [Fact]
    public async Task Email_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("username", "invalidemail", "password", "password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.Email)));
    }

    [Fact]
    public async Task Password_IsEmpty_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "", "password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.Password)));
    }

    [Fact]
    public async Task Password_IsShortInLength_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "p", "p");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.Password)));
    }

    [Fact]
    public async Task RetypePassword_IsEmpty_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.RetypePassword)));
    }

    [Fact]
    public async Task RetypePassword_DidNotMatchPassword_ReturnsAnError()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "different-password");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(RegisterUserCommand.RetypePassword)));
    }
}
