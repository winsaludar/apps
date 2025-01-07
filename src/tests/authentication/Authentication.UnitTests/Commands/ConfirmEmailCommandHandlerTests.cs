namespace Authentication.UnitTests.Commands;

public class ConfirmEmailCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly ConfirmEmailCommandHandler _handler;

    public ConfirmEmailCommandHandlerTests()
    {
        _handler = new(_userRepository.Object);
    }

    [Fact]
    public async Task Handle_EmailDoesNotExist_ThrowsBadRequestException()
    {
        // Arrange
        ConfirmEmailCommand command = new("email@example.com", "token");
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_EmailAlreadyVerified_ThrowsBadRequestException()
    {
        // Arrange
        ConfirmEmailCommand command = new("email@example.com", "token");
        User user = User.Create("username", command.Email, Guid.NewGuid(), true);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_TokenIsInvalid_ThrowsBadRequestException()
    {
        // Arrange
        ConfirmEmailCommand command = new("email@example.com", "token");
        User user = User.Create("username", command.Email, Guid.NewGuid(), false);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _userRepository.Setup(x => x.ConfirmEmailAsync(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }
}
