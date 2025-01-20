namespace Budget.UnitTests.Expenses.Commands;

public sealed class DeleteExpenseCommandHandlerUnitTests
{
    private readonly Mock<IExpenseDbContext> _dbContext = new();
    private readonly DeleteExpenseCommandHandler _handler;

    public DeleteExpenseCommandHandlerUnitTests()
    {
        _handler = new(_dbContext.Object);
    }

    [Fact]
    public async Task Handle_DeleteExpenseFails_ThrowsExpenseException()
    {
        // Arrange
        DeleteExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid());
        _dbContext.Setup(x => x.DeleteExpenseAsync(It.IsAny<Guid>(), It.IsAny<Guid>()))
            .ThrowsAsync(new ExpenseException("Invalid expense"));

        // Act
        await Assert.ThrowsAsync<ExpenseException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _dbContext.Verify(x => x.DeleteExpenseAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never());
    }

    [Fact]
    public async Task Handle_DeleteExpenseSucceed_SaveChanges()
    {
        // Arrange
        DeleteExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid());
        _dbContext.Setup(x => x.DeleteExpenseAsync(It.IsAny<Guid>(), It.IsAny<Guid>()));
        _dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _dbContext.Verify(x => x.DeleteExpenseAsync(It.IsAny<Guid>(), It.IsAny<Guid>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}
