namespace Authentication.UnitTests.Commands;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _handler = new(_userRepository.Object);
    }

    [Fact]
    public async Task Handle_EmailAlreadyExist_ThrowsBadRequestException()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "retypePassword");
        User user = User.Create(command.Username, command.Email, command.Password);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UsernameAlreadyExist_ThrowsBadRequestException()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "retypePassword");
        User user = User.Create(command.Username, command.Email, command.Password);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_PasswordDidNotMeetRequirements_ThrowsBadRequestException()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "retypePassword");
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.ValidateRegisterPasswordAsync(It.IsAny<string>()))
            .ReturnsAsync(false);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RegistrationSuccessful_ReturnsUserWithId()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "retypePassword");
        Guid newId = Guid.NewGuid();
        User newUser = User.Create(command.Username, command.Email, command.Password, newId);        
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.ValidateRegisterPasswordAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        _userRepository.Setup(x => x.RegisterAsync(It.IsAny<User>()))
            .ReturnsAsync(newId);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepository.Verify(x => x.RegisterAsync(It.IsAny<User>()), Times.Once);
        Assert.NotNull(result);
        Assert.IsType<User>(result);
        Assert.Equal(result.Id, newId);
        Assert.Equal(result.Username, command.Username);
        Assert.Equal(result.Email, command.Email);        
    }
}
