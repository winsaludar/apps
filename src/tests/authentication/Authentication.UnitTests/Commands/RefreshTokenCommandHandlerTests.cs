namespace Authentication.UnitTests.Commands;

public class RefreshTokenCommandHandlerTests
{
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<ITokenRepository> _tokenRepository = new();
    private readonly Mock<IRefreshTokenRepository> _refreshTokenRepository = new();
    private readonly RefreshTokenCommandHandler _handler;

    public RefreshTokenCommandHandlerTests()
    {
        _handler = new RefreshTokenCommandHandler(_userRepository.Object, _tokenRepository.Object, _refreshTokenRepository.Object);
    }

    [Fact]
    public async Task Handle_RefreshTokenIsInvalid_ThrowsBadRequestException()
    {
        // Arrange
        RefreshTokenCommand command = new("token", "refresh-token");
        _refreshTokenRepository.Setup(x => x.GetByOldRefreshTokenAsync(It.IsAny<string>()))
            .ReturnsAsync((RefreshToken)null!);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RefreshTokenIsRevoked_ThrowsBadRequestException()
    {
        // Arrange
        RefreshTokenCommand command = new("token", "refresh-token");
        RefreshToken refreshToken = RefreshToken.Create(command.Token, "jwtid", true, "userid", DateTime.UtcNow, DateTime.UtcNow);
        _refreshTokenRepository.Setup(x => x.GetByOldRefreshTokenAsync(It.IsAny<string>()))
            .ReturnsAsync(refreshToken);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_UserDoesNotExist_ThrowsBadRequestExcption()
    {
        // Arrange
        RefreshTokenCommand command = new("token", "refresh-token");
        RefreshToken refreshToken = RefreshToken.Create(command.Token, "jwtid", false, "userid", DateTime.UtcNow, DateTime.UtcNow);
        _refreshTokenRepository.Setup(x => x.GetByOldRefreshTokenAsync(It.IsAny<string>()))
            .ReturnsAsync(refreshToken);
        _userRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync((User)null!);

        // Act & Assert
        await Assert.ThrowsAsync<BadRequestException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_GeneratedTokenIsNull_ThrowsTokenException()
    {
        // Arrange
        RefreshTokenCommand command = new("token", "refresh-token");
        RefreshToken refreshToken = RefreshToken.Create(command.Token, "jwtid", false, "userid", DateTime.UtcNow, DateTime.UtcNow);
        User user = User.Create("username", "email");
        _refreshTokenRepository.Setup(x => x.GetByOldRefreshTokenAsync(It.IsAny<string>()))
            .ReturnsAsync(refreshToken);
        _userRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _tokenRepository.Setup(x => x.GenerateJwtAsync(It.IsAny<User>(), It.IsAny<RefreshToken>()))
            .ReturnsAsync((Token)null!);

        // Act & Assert
        await Assert.ThrowsAsync<TokenException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_RefreshTokenSuccesful_ReturnsUserAndToken()
    {
        // Arrange
        RefreshTokenCommand command = new("token", "refresh-token");
        RefreshToken refreshToken = RefreshToken.Create(command.Token, "jwtid", false, "userid", DateTime.UtcNow, DateTime.UtcNow);
        User user = User.Create("username", "email");
        Token token = Token.Create("token", "refresh-token", DateTime.UtcNow);
        _refreshTokenRepository.Setup(x => x.GetByOldRefreshTokenAsync(It.IsAny<string>()))
            .ReturnsAsync(refreshToken);
        _userRepository.Setup(x => x.GetByUserIdAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        _tokenRepository.Setup(x => x.RefreshJwtAsync(It.IsAny<Token>(), It.IsAny<User>(), It.IsAny<RefreshToken>()))
            .ReturnsAsync(token);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _tokenRepository.Verify(x => x.RefreshJwtAsync(It.IsAny<Token>(), It.IsAny<User>(), It.IsAny<RefreshToken>()), Times.Once);
        Assert.IsType<ValueTuple<User, Token>>(result);
        Assert.IsType<User>(result.Item1);
        Assert.IsType<Token>(result.Item2);
        Assert.NotEmpty(result.Item2.Value); // Should have a token
        Assert.NotEmpty(result.Item2.RefreshToken); // Should have a refresh token
    }
}
