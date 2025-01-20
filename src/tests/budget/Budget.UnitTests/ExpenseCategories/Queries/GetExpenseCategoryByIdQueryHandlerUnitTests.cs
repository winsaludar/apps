namespace Budget.UnitTests.ExpenseCategories.Queries;

public sealed class GetExpenseCategoryByIdQueryHandlerUnitTests
{
    private readonly Mock<IExpenseCategoryRepository> _repository = new();
    private readonly GetExpenseCategoryByIdQueryHandler _handler;

    public GetExpenseCategoryByIdQueryHandlerUnitTests()
    {
        _handler = new(_repository.Object);
    }

    [Fact]
    public async Task Handle_ExpenseCategoryDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        GetExpenseCategoryByIdQuery query = new(Guid.NewGuid());
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync((ExpenseCategoryDetailDto)null!);

        // Act && Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ExpenseCategoryExist_ReturnExpenseCategoryDetailDto()
    {
        // Arrange
        ExpenseCategoryDetailDto expenseCategory = new(Guid.NewGuid(), "Test Category", "Test Category Description", Guid.NewGuid(), DateTime.UtcNow);
        GetExpenseCategoryByIdQuery query = new(expenseCategory.Id);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(expenseCategory);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), CancellationToken.None), Times.Once());
        Assert.IsType<ExpenseCategoryDetailDto>(result);
    }
}
