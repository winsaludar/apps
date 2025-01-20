namespace Budget.UnitTests.Expenses.Commands;

public sealed class CreateExpenseCommandHandlerTests
{
    private readonly Mock<IExpenseDbContext> _dbContext = new();
    private readonly CreateExpenseCommandHandler _handler;

    public CreateExpenseCommandHandlerTests()
    {
        _handler = new(_dbContext.Object);
    }

    [Fact]
    public async Task Handle_AddExpenseFails_ThrowsExpenseException()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());
        _dbContext.Setup(x => x.AddExpenseAsync(It.IsAny<Expense>()))
            .ThrowsAsync(new ExpenseException("Invalid expense category"));

        // Act
        await Assert.ThrowsAsync<ExpenseException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _dbContext.Verify(x => x.AddExpenseAsync(It.IsAny<Expense>()),Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never());
    }

    [Fact]
    public async Task Handle_AddExpenseSucceed_SaveChangesAndReturnNewId()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());
        _dbContext.Setup(x => x.AddExpenseAsync(It.IsAny<Expense>()));
        _dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
        _dbContext.Verify(x => x.AddExpenseAsync(It.IsAny<Expense>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());        
    }
}
