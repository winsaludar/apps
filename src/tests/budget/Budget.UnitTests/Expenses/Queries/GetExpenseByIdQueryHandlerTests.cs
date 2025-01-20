namespace Budget.UnitTests.Expenses.Queries;

public sealed class GetExpenseByIdQueryHandlerTests
{
    private readonly Mock<IExpenseRepository> _repository = new();
    private readonly GetExpenseByIdQueryHandler _handler;

    public GetExpenseByIdQueryHandlerTests()
    {
        _handler = new(_repository.Object);
    }

    [Fact]
    public async Task Handle_ExpenseDoesNotExist_ThrowsNotFoundException()
    {
        // Arrange
        GetExpenseByIdQuery query = new(Guid.NewGuid(), Guid.NewGuid());
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync((ExpenseDetailDto)null!);

        // Act && Assert
        await Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_ExpenseExist_ReturnExpenseDetailDto()
    {
        // Arrange
        ExpenseDetailDto expense = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, "Test Description", Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid(), "Test Category Name", "Test Category Description");
        GetExpenseByIdQuery query = new(expense.Id, expense.UserId);
        _repository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), CancellationToken.None))
            .ReturnsAsync(expense);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        _repository.Verify(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), CancellationToken.None), Times.Once());
        Assert.IsType<ExpenseDetailDto>(result);
    }
}
