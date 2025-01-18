namespace Budget.Domain.Expenses;

public sealed class Expense : Entity
{
    public Guid UserId { get; private set; }
    public decimal Amount { get; private set; }
    public string Currency { get; private set; } = default!;
    public DateTime Date { get; private set; }
    public ExpenseCategory Category { get; private set; } = default!;
    public string Description { get; private set; } = default!;

    public Expense(Guid userId, decimal amount, string currency, DateTime date, ExpenseCategory category, string description)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        Amount = amount;
        Currency = currency;
        Date = date;
        Category = category;
        Description = description;
        CreatedBy = userId;
    }

    public void Update(Guid userId, decimal amount, string currency, DateTime date, ExpenseCategory category, string description)
    {
        Amount = amount;
        Currency = currency;
        Date = date;
        Category = category;
        Description = description;
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }
}
