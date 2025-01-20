namespace Budget.UnitTests.ExpenseCategories.Commands;

public sealed class DeleteExpenseCategoryCommandHandlerUnitTests
{
    private readonly Mock<IExpenseCategoryDbContext> _dbContext = new();
    private readonly DeleteExpenseCategoryCommandHandler _handler;

    public DeleteExpenseCategoryCommandHandlerUnitTests()
    {
        _handler = new(_dbContext.Object);
    }

    [Fact]
    public async Task Handle_DeleteExpenseFails_ThrowsExpenseException()
    {
        // Arrange
        DeleteExpenseCategoryCommand command = new(Guid.NewGuid());
        _dbContext.Setup(x => x.DeleteExpenseCategoryAsync(It.IsAny<Guid>()))
            .ThrowsAsync(new ExpenseException("Invalid expense category"));

        // Act
        await Assert.ThrowsAsync<ExpenseException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _dbContext.Verify(x => x.DeleteExpenseCategoryAsync(It.IsAny<Guid>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never());
    }

    [Fact]
    public async Task Handle_DeleteExpenseSucceed_SaveChanges()
    {
        // Arrange
        DeleteExpenseCategoryCommand command = new(Guid.NewGuid());
        _dbContext.Setup(x => x.DeleteExpenseCategoryAsync(It.IsAny<Guid>()));
        _dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _dbContext.Verify(x => x.DeleteExpenseCategoryAsync(It.IsAny<Guid>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}
