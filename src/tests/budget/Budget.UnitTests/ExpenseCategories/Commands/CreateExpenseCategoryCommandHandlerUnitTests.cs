namespace Budget.UnitTests.ExpenseCategories.Commands;

public sealed class CreateExpenseCategoryCommandHandlerUnitTests
{
    private readonly Mock<IExpenseCategoryDbContext> _dbContext = new();
    private readonly CreateExpenseCategoryCommandHandler _handler;

    public CreateExpenseCategoryCommandHandlerUnitTests()
    {
        _handler = new(_dbContext.Object);
    }

    [Fact]
    public async Task Handle_AddExpenseCategoryFails_ThrowsExpenseException()
    {
        // Arrange
        CreateExpenseCategoryCommand command = new(Guid.NewGuid(), "Already Exist", "Already EXist");
        _dbContext.Setup(x => x.AddExpenseCategoryAsync(It.IsAny<ExpenseCategory>()))
            .ThrowsAsync(new ExpenseException("Name already exist"));

        // Act && Assert
        await Assert.ThrowsAsync<ExpenseException>(() => _handler.Handle(command, CancellationToken.None));
        _dbContext.Verify(x => x.AddExpenseCategoryAsync(It.IsAny<ExpenseCategory>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Never());
    }

    [Fact]
    public async Task Handle_AddExpenseCategorySucceed_SaveChangesAndReturnNewId()
    {
        // Arrange
        CreateExpenseCategoryCommand command = new(Guid.NewGuid(), "Test Category", "Test Category Description");
        _dbContext.Setup(x => x.AddExpenseCategoryAsync(It.IsAny<ExpenseCategory>()));

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsType<Guid>(result);
        Assert.NotEqual(Guid.Empty, result);
        _dbContext.Verify(x => x.AddExpenseCategoryAsync(It.IsAny<ExpenseCategory>()), Times.Once());
        _dbContext.Verify(x => x.SaveChangesAsync(CancellationToken.None), Times.Once());
    }
}
