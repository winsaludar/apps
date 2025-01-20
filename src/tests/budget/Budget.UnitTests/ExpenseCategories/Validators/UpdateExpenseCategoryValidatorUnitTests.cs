namespace Budget.UnitTests.ExpenseCategories.Validators;

public sealed class UpdateExpenseCategoryValidatorUnitTests
{
    private readonly UpdateExpenseCategoryValidator _validator = new();

    [Fact]
    public async Task ExpenseCategoryId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.Empty, Guid.NewGuid(), "Test Category", "Test Category Description");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCategoryCommand.ExpenseCategoryId)));
    }

    [Fact]
    public async Task UserId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.NewGuid(), Guid.Empty, "Test Category", "Test Category Description");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCategoryCommand.UserId)));
    }

    [Fact]
    public async Task Name_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.NewGuid(), Guid.NewGuid(), "", "Test Category Description");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCategoryCommand.Name)));
    }

    [Fact]
    public async Task Description_IsEmpty_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.NewGuid(), Guid.NewGuid(), "Test Category", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCategoryCommand.Description)));
    }

    [Fact]
    public async Task ParentCategoryId_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        UpdateExpenseCategoryCommand command = new(Guid.NewGuid(), Guid.NewGuid(), "Test Category", "Test Category Description", "111");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(UpdateExpenseCategoryCommand.ParentCategoryId)));
    }
}
