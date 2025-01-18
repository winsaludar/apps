namespace Budget.Domain.Expenses;

public sealed class Expense : Entity
{
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = default!;
    public DateTime Date { get; private set; }
    public string Description { get; private set; } = default!;
    public Guid CategoryId { get; private set; }

    // Navigation Property
    public ExpenseCategory Category { get; private set; } = default!;

    public Expense(Guid userId, decimal amount, string currency, DateTime date, string description, Guid categoryId)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Currency = currency;
        Date = date;
        Description = description;
        CategoryId = categoryId;
        CreatedBy = userId;
    }

    public void Update(Guid userId, decimal amount, string currency, DateTime date, string description, Guid categoryId)
    {
        Amount = amount;
        Currency = currency;
        Date = date;
        Description = description;
        CategoryId = categoryId;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }
}
