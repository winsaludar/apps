namespace Budget.UnitTests.ExpenseCategories.Queries;

public sealed class GetExpenseCategoriesQueryHandlerUnitTests
{
    private readonly Mock<IExpenseCategoryRepository> _repository = new();
    private readonly GetExpenseCategoriesQueryHandler _handler;

    public GetExpenseCategoriesQueryHandlerUnitTests()
    {
        _handler = new(_repository.Object);
    }

    [Fact]
    public async Task Handle_ExpenseCategoriesDoesNotExist_ReturnEmptySet()
    {
        // Arrange
        GetExpenseCategoriesQuery query = new();
        _repository.Setup(x => x.GetAllAsync(CancellationToken.None))
            .ReturnsAsync([]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.GetAllAsync(CancellationToken.None), Times.Once());
        Assert.IsType<List<ExpenseCategorySummaryDto>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public async Task Handle_ExpenseCategoriesExist_ReturnExpenseCategorySummaryDtos()
    {
        // Arrange
        GetExpenseCategoriesQuery query = new();
        _repository.Setup(x => x.GetAllAsync(CancellationToken.None))
            .ReturnsAsync([
                new ExpenseCategorySummaryDto(Guid.NewGuid(), "Category #1", "Category #1 Description"),
                new ExpenseCategorySummaryDto(Guid.NewGuid(), "Category #2", "Category #2 Description"),
                new ExpenseCategorySummaryDto(Guid.NewGuid(), "Category #3", "Category #3 Description")]);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.GetAllAsync(CancellationToken.None), Times.Once());
        Assert.IsType<List<ExpenseCategorySummaryDto>>(result);
        Assert.NotEmpty(result);
        Assert.Equal(3, result.Count);
    }
}
