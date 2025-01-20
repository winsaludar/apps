namespace Budget.UnitTests.Expenses.Queries;

public sealed class GetExpensesQueryHandlerTests
{
    private readonly Mock<IExpenseRepository> _repository = new();
    private readonly GetExpensesQueryHandler _handler;

    public GetExpensesQueryHandlerTests()
    {
        _handler = new(_repository.Object);
    }

    [Fact]
    public async Task Handle_ExpensesDoesNotExist_ReturnEmptySet()
    {
        // Arrange
        GetExpensesQuery query = new(Guid.NewGuid());
        _repository.Setup(x => x.GetAllAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync([]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.GetAllAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once());
        Assert.IsType<List<ExpenseSummaryDto>>(result);
    }

    [Fact]
    public async Task Handle_ExpensesExist_ReturnExpenseSummaryDtos()
    {
        // Arrange
        GetExpensesQuery query = new(Guid.NewGuid());
        _repository.Setup(x => x.GetAllAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync([
                new ExpenseSummaryDto(Guid.NewGuid(), Guid.NewGuid(), 1001, "PHP", "Test Expense #1", DateTime.UtcNow, Guid.NewGuid(), "Test Category #1"),
                new ExpenseSummaryDto(Guid.NewGuid(), Guid.NewGuid(), 1002, "PHP", "Test Expense #2", DateTime.UtcNow, Guid.NewGuid(), "Test Category #2"),
                new ExpenseSummaryDto(Guid.NewGuid(), Guid.NewGuid(), 1003, "PHP", "Test Expense #3", DateTime.UtcNow, Guid.NewGuid(), "Test Category #3")]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.GetAllAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once());
        Assert.IsType<List<ExpenseSummaryDto>>(result);
        Assert.Equal(3, result.Count);
    }
}
