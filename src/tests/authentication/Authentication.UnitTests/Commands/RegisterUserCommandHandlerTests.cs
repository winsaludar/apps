using Authentication.Core.Models;

namespace Authentication.UnitTests.Commands;

public class RegisterUserCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<ITokenRepository> _tokenRepository = new();
    private readonly Mock<IEmailService> _emailService = new();

    private readonly RegisterUserCommandHandler _handler;

    public RegisterUserCommandHandlerTests()
    {
        _handler = new(_userRepository.Object, _tokenRepository.Object, _emailService.Object);
    }

    [Fact]
    public async Task Handle_EmailAlreadyExist_ThrowsBadRequestException()
    {
        // Arrange
        RegisterUserCommand command = new("username", "email@email.com", "password", "retypePassword");
        User user = User.Create(command.Username, command.Email);
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
        User user = User.Create(command.Username, command.Email);
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
        User newUser = User.Create(command.Username, command.Email, newId);
        _userRepository.Setup(x => x.GetByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.GetByUsernameAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);
        _userRepository.Setup(x => x.ValidateRegisterPasswordAsync(It.IsAny<string>()))
            .ReturnsAsync(true);
        _userRepository.Setup(x => x.RegisterAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(newId);
        _tokenRepository.Setup(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("token");

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _userRepository.Verify(x => x.RegisterAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        _tokenRepository.Verify(x => x.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()), Times.Once);
        _emailService.Verify(x => x.SendEmailConfirmation(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        Assert.NotNull(result);
        Assert.IsType<User>(result);
        Assert.Equal(result.Id, newId);
        Assert.Equal(result.Username, command.Username);
        Assert.Equal(result.Email, command.Email);        
    }
}
