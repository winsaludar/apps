namespace Budget.UnitTests.ExpenseCategories.Domain;

public sealed class ExpenseCategoryUnitTests
{
    [Fact]
    public void ExpenseCategoryId_IsEmptyGuid_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new ExpenseCategory(Guid.Empty, "Test Category", "Test Category Description", Guid.NewGuid()));
    }

    [Theory]
    [InlineData(null!)]
    [InlineData(" ")]
    public void Name_IsEmptyOrWhitespace_ThrowsExpenseException(string? name)
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new ExpenseCategory(Guid.NewGuid(), name!, "Test Category Description", Guid.NewGuid()));
    }

    [Theory]
    [InlineData(null!)]
    [InlineData(" ")]
    public void Description_IsEmptyOrWhitespace_ThrowsExpenseException(string? description)
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new ExpenseCategory(Guid.NewGuid(), "Test Category", description!, Guid.NewGuid()));
    }

    [Fact]
    public void ParentCategoryId_IsEmptyGuid_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new ExpenseCategory(Guid.NewGuid(), "Test Category", "Test Category Description", Guid.NewGuid(), Guid.Empty));
    }

    [Fact]
    public void ParentCategoryId_IsTheSameWithSelfId_ThrowsExpenseException()
    {
        // Arrange
        Guid id = Guid.NewGuid();

        // Act, && Assert
        Assert.Throws<ExpenseException>(() => new ExpenseCategory(id, "Test Category", "Test Category Description", Guid.NewGuid(), id));
    }
}
