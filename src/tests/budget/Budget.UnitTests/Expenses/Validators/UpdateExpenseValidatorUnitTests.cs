namespace Budget.UnitTests.Expenses.Validators;

public sealed class UpdateExpenseValidatorUnitTests
{
    public readonly UpdateExpenseValidator _validator = new();

    [Fact]
    public async Task ExpenseId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.Empty, Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.ExpenseId)));
    }

    [Fact]
    public async Task UserId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.Empty, 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.UserId)));
    }

    [Fact]
    public async Task Amount_IsLessThanZero_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), -1, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.Amount)));
    }

    [Fact]
    public async Task Currency_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.Currency)));
    }

    [Fact]
    public async Task Date_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", "", "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.Date)));
    }

    [Fact]
    public async Task Date_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", "1111", "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.Date)));
    }

    [Fact]
    public async Task Description_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.Description)));
    }

    [Fact]
    public async Task CategoryId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Description", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.CategoryId)));
    }

    [Fact]
    public async Task CategoryId_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCommand command = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Description", "1111");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCommand.CategoryId)));
    }
}
