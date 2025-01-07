namespace Authentication.UnitTests.Commands;

public class ResetPasswordCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly ResetPasswordCommandHandler _handler;

    public ResetPasswordCommandHandlerTests()
    {
        _handler = new(_userRepository.Object);
    }

    [Fact]
    public async Task Handle_EmailDoesNotExist_ThrowsBadRequestException()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "password", "password", "token");
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_EmailNotVerified_ThrowsBadRequestException()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "password", "password", "token");
        User user = User.Create("username", command.Email, Guid.NewGuid(), false);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_PasswordDidNotMeetRequirements_ThrowsBadRequestException()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "password", "password", "token");
        User user = User.Create("username", command.Email, Guid.NewGuid(), true);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userRepository.Setup(x => x.ValidateRegisterPasswordAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_TokenIsInvalid_ThrowsBadRequestException()
    {
        // Arrange
        ResetPasswordCommand command = new("email@example.com", "password", "password", "token");
        User user = User.Create("username", command.Email, Guid.NewGuid(), true);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userRepository.Setup(x => x.ValidateRegisterPasswordAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        _userRepository.Setup(x => x.ResetPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
