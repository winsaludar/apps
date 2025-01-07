namespace Authentication.UnitTests.Commands;

public class LoginUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<ITokenRepository> _tokenRepository = new();
    private readonly LoginUserCommandHandler _handler;

    public LoginUserCommandHandlerTests()
    {
        _handler = new LoginUserCommandHandler(_userRepository.Object, _tokenRepository.Object);
    }

    [Fact]
    public async Task Handle_EmailDoesNotExist_ThrowsBadRequestException()
    {
        // Arrange
        LoginUserCommand command = new("email@example.com", "password");
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_PasswordIsIncorrect_ThrowsBadRequestException()
    {
        // Arrange
        LoginUserCommand command = new("email@example.com", "password");
        User user = User.Create("username", command.Email, command.Password, Guid.NewGuid(), false);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userRepository.Setup(x => x.ValidateLoginPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_EmailIsNotYetValidated_ThrowsUnauthorizedAccessException()
    {
        // Arrange
        LoginUserCommand command = new("email@example.com", "password");
        User user = User.Create("username", command.Email, command.Password, Guid.NewGuid(), false);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userRepository.Setup(x => x.ValidateLoginPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);

        // Act & Assert
        await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_LoginSuccessful_ReturnsUserAndToken()
    {
        // Arrange
        LoginUserCommand command = new("email@example.com", "password");
        User user = User.Create("username", command.Email, command.Password, Guid.NewGuid(), true);
        Token token = Token.Create("value", "refresh-token", DateTime.UtcNow);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userRepository.Setup(x => x.ValidateLoginPasswordAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        _tokenRepository.Setup(x => x.GenerateJwtAsync(It.IsAny<User>(), It.IsAny<RefreshToken>()))
            .ReturnsAsync(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _tokenRepository.Verify(x => x.GenerateJwtAsync(It.IsAny<User>(), It.IsAny<RefreshToken>()), Times.Once());
        Assert.IsType<ValueTuple<User,Token>>(result);
        Assert.IsType<User>(result.Item1);
        Assert.IsType<Token>(result.Item2);
        Assert.NotEmpty(result.Item2.Value); // Should have a token
        Assert.NotEmpty(result.Item2.RefreshToken); // Should have a refresh token
    }
}
