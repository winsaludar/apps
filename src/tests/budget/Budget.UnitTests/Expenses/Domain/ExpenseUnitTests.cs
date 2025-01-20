namespace Budget.UnitTests.Expenses.Domain;

public sealed class ExpenseUnitTests
{
    [Fact]
    public void ExpenseId_IsEmptyGuid_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.Empty, Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, "Test Expense", Guid.NewGuid()));
    }

    [Fact]
    public void ExpenseId_IsValid_SetExpenseId()
    {
        // Arrange
        Guid expenseId = Guid.NewGuid();
        
        // Act
        Expense expense = new(expenseId, Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, "Test Expense", Guid.NewGuid());

        // Assert
        Assert.Equal(expenseId, expense.Id);
    }

    [Fact]
    public void UserId_IsEmptyGuid_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.NewGuid(), Guid.Empty, 1000, "PHP", DateTime.UtcNow, "Test Expense", Guid.NewGuid()));
    }

    [Fact]
    public void UserId_IsValid_SetUserId()
    {
        // Arrange
        Guid userId = Guid.NewGuid();

        // Act
        Expense expense = new(Guid.NewGuid(), userId, 1000, "PHP", DateTime.UtcNow, "Test Expense", Guid.NewGuid());

        // Assert
        Assert.Equal(userId, expense.UserId);
    }

    [Fact]
    public void Amount_IsLessThanZero_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.NewGuid(), Guid.NewGuid(), -1, "PHP", DateTime.UtcNow, "Test Expense", Guid.NewGuid()));
    }

    [Fact]
    public void Amount_IsValid_SetAmount()
    {
        // Arrange
        decimal amount = 1000;

        // Act
        Expense expense = new(Guid.NewGuid(), Guid.NewGuid(), amount, "PHP", DateTime.UtcNow, "Test Expense", Guid.NewGuid());

        // Assert
        Assert.Equal(amount, expense.Amount);
    }

    [Theory]
    [InlineData(null!)]
    [InlineData(" ")]
    public void Currency_IsEmptyOrWhitespace_ThrowsExpenseException(string? currency)
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.NewGuid(), Guid.NewGuid(), 1000, currency!, DateTime.UtcNow, "Test Expense", Guid.NewGuid()));
    }

    [Fact]
    public void Currency_IsValid_SetCurrency()
    {
        // Arrange
        string currency = "PHP";

        // Act
        Expense expense = new(Guid.NewGuid(), Guid.NewGuid(), 1000, currency, DateTime.UtcNow, "Test Expense", Guid.NewGuid());

        // Assert
        Assert.Equal(currency, expense.Currency);
    }

    [Fact]
    public void Date_IsInTheFuture_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow.AddDays(1), "Test Expense", Guid.NewGuid()));
    }

    [Fact]
    public void Date_IsValid_SetDate()
    {
        // Arrange
        DateTime date = DateTime.UtcNow;

        // Act
        Expense expense = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", date, "Test Expense", Guid.NewGuid());

        // Assert
        Assert.Equal(date, expense.Date);
    }

    [Theory]
    [InlineData(null!)]
    [InlineData(" ")]
    public void Description_IsEmptyOrWhitespace_ThrowsExpenseException(string? description)
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, description!, Guid.NewGuid()));
    }

    [Fact]
    public void Description_IsValid_SetDescription()
    {
        // Arrange
        string description = "Test Expense";

        // Act
        Expense expense = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, description, Guid.NewGuid());

        // Assert
        Assert.Equal(description, expense.Description);
    }

    [Fact]
    public void CategoryId_IsEmptyGuid_ThrowsExpenseException()
    {
        // Arrange, Act, && Assert
        Assert.Throws<ExpenseException>(() => new Expense(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, "Test Expense", Guid.Empty));
    }

    [Fact]
    public void CategoryId_IsValid_SetCategoryId()
    {
        // Arrange
        Guid categoryId = Guid.NewGuid();

        // Act
        Expense expense = new(Guid.NewGuid(), Guid.NewGuid(), 1000, "PHP", DateTime.UtcNow, "Test Expense", categoryId);

        // Assert
        Assert.Equal(categoryId, expense.CategoryId);
    }
}
