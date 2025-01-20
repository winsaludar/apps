namespace Budget.UnitTests.Expenses.Validators;

public sealed class DeleteExpenseValidatorUnitTests
{
    public readonly DeleteExpenseValidator _validator = new();

    [Fact]
    public async Task ExpenseId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        DeleteExpenseCommand command = new(Guid.Empty, Guid.NewGuid());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(DeleteExpenseCommand.ExpenseId)));
    }

    [Fact]
    public async Task UserId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        DeleteExpenseCommand command = new(Guid.NewGuid(), Guid.Empty);


        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(DeleteExpenseCommand.UserId)));
    }
}
