namespace Budget.UnitTests.Expenses.Validators;

public sealed class CreateExpenseValidatorUnitTests
{
    private readonly CreateExpenseValidator _validator = new();

    [Fact]
    public async Task UserId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.Empty, 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.UserId)));
    }

    [Fact]
    public async Task Amount_IsLessThanZero_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), -1, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.Amount)));
    }

    [Fact]
    public async Task Currency_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "", DateTime.UtcNow.ToShortDateString(), "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.Currency)));
    }

    [Fact]
    public async Task Date_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", "", "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.Date)));
    }

    [Fact]
    public async Task Date_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", "1111", "Test Expense", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.Date)));
    }

    [Fact]
    public async Task Description_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "", Guid.NewGuid().ToString());

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.Description)));
    }

    [Fact]
    public async Task CategoryId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Description", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.CategoryId)));
    }

    [Fact]
    public async Task CategoryId_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCommand command = new(Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.ToShortDateString(), "Test Description", "1111");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCommand.CategoryId)));
    }
}
