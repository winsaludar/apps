namespace Authentication.UnitTests.Commands;

public class ResendEmailConfirmationCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<ITokenRepository> _tokenRepository = new();
    private readonly Mock<IEmailService> _emailService = new();
    private readonly ResendEmailConfirmationCommandHandler _handler;

    public ResendEmailConfirmationCommandHandlerTests()
    {
        _handler = new(_userRepository.Object, _tokenRepository.Object, _emailService.Object);
    }

    [Fact]
    public async Task Handle_EmailDoesNotExist_ThrowsBadRequestException()
    {
        // Arrange
        ResendEmailConfirmationCommand command = new("email@example.com");
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_EmailAlreadyVerified_ThrowsBadRequestException()
    {
        // Arrange
        ResendEmailConfirmationCommand command = new("email@example.com");
        User user = User.Create("username", command.Email, Guid.NewGuid(), true);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GeneratedTokenIsNull_ThrowsTokenException()
    {
        // Arrange
        ResendEmailConfirmationCommand command = new("email@example.com");
        User user = User.Create("username", command.Email, Guid.NewGuid(), false);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _tokenRepository.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
            .ReturnsAsync((string)null!);

        // Act & Assert
        await Assert.ThrowsAsync<TokenException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ResendSuccessful_SendEmail()
    {
        // Arrange
        ResendEmailConfirmationCommand command = new("email@example.com");
        User user = User.Create("username", command.Email, Guid.NewGuid(), false);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _tokenRepository.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("token");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _emailService.Verify(x => x.SendEmailConfirmation(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once());
    }
}
