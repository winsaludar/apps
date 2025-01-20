namespace Budget.UnitTests.ExpenseCategories.Validators;

public sealed class DeleteExpenseCategoryValidatorUnitTests
{
    private readonly DeleteExpenseCategoryValidator _validator = new();

    [Fact]
    public async Task ExpenseCategoryId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        DeleteExpenseCategoryCommand command = new(Guid.Empty);

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(DeleteExpenseCategoryCommand.Id)));
    }
}
