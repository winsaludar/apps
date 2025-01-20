namespace Budget.UnitTests.ExpenseCategories.Commands;

public sealed class UpdateExpenseCategoryCommandHandleUnitTests
{
    private readonly Mock<IExpenseCategoryDbContext> _dbContext = new();
    private readonly UpdateExpenseCategoryCommandHandler _handler;

    public UpdateExpenseCategoryCommandHandleUnitTests()
    {
        _handler = new(_dbContext.Object);
    }

    [Fact]
    public async Task Handle_UpdateExpenseCategoryFails_ThrowsExpenseException()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.NewGuid(), Guid.NewGuid(), "Test Category", "Test Category Description");
        _dbContext.Setup(x => x.UpdateExpenseCategoryAsync(It.IsAny<ExpenseCategory>()))
            .ThrowsAsync(new ExpenseException("Invalid expense category name"));

        // Act
        await Assert.ThrowsAsync<ExpenseException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _dbContext.Verify(x => x.UpdateExpenseCategoryAsync(It.IsAny<ExpenseCategory>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never());
    }

    [Fact]
    public async Task Handle_UpdateExpenseCategorySucceed_SaveChanges()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.NewGuid(), Guid.NewGuid(), "Test Category", "Test Category Description");
        _dbContext.Setup(x => x.UpdateExpenseCategoryAsync(It.IsAny<ExpenseCategory>()));
        _dbContext.Setup(x => x.SaveChangesAsync(CancellationToken.None));

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _dbContext.Verify(x => x.UpdateExpenseCategoryAsync(It.IsAny<ExpenseCategory>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}
