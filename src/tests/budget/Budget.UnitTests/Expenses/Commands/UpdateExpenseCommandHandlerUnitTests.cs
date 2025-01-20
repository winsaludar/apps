namespace Budget.UnitTests.Expenses.Commands;

public sealed class UpdateExpenseCommandHandlerUnitTests
{
    public readonly Mock<IExpenseDbContext> _dbContext = new();
    public readonly UpdateExpenseCommandHandler _handler;

    public UpdateExpenseCommandHandlerUnitTests()
    {
        _handler = new(_dbContext.Object);
    }

    [Fact]
    public async Task Handle_UpdateExpenseFails_ThrowsExpenseException()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());
        _dbContext.Setup(x => x.UpdateExpenseAsync(It.IsAny<Expense>()))
            .ThrowsAsync(new ExpenseException("Invalid expense"));

        // Act
        await Assert.ThrowsAsync<ExpenseException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _dbContext.Verify(x => x.UpdateExpenseAsync(It.IsAny<Expense>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never());
    }

    [Fact]
    public async Task Handle_UpdateExpenseSucceed_SaveChanges()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());
        _dbContext.Setup(x => x.UpdateExpenseAsync(It.IsAny<Expense>()));
        _dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _dbContext.Verify(x => x.UpdateExpenseAsync(It.IsAny<Expense>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}
