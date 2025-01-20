using FluentValidation;

namespace Budget.UnitTests.ExpenseCategories.Validators;

public sealed class CreateExpenseCategoryValidatorUnitTests
{
    public readonly CreateExpenseCategoryValidator _validator = new();

    [Fact]
    public async Task UserId_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCategoryCommand command = new(Guid.Empty, "Test Category", "Test Category Description");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCategoryCommand.UserId)));
    }

    [Fact]
    public async Task Name_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCategoryCommand command = new(Guid.NewGuid(), "", "Test Category Description");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCategoryCommand.Name)));
    }

    [Fact]
    public async Task Description_IsEmpty_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCategoryCommand command = new(Guid.NewGuid(), "Test Category", "");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCategoryCommand.Description)));
    }

    [Fact]
    public async Task ParentCategoryId_IsInvalidFormat_ReturnsAnError()
    {
        // Arrange
        CreateExpenseCategoryCommand command = new(Guid.NewGuid(), "Test Category", "Test Category Description", "111");

        // Act
        var result = await _validator.ValidateAsync(command);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.True(result.Errors?.Any(x => x.PropertyName == nameof(CreateExpenseCategoryCommand.ParentCategoryId)));
    }
}
