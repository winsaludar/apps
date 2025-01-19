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

    public Expense(Guid id, Guid userId, decimal amount, string currency, DateTime date, string description, Guid categoryId)
    {
        SetId(id);
        SetUserId(userId);
        SetAmount(amount);
        SetCurrency(currency);
        SetDate(date);
        SetDescription(description);
        SetCategoryId(categoryId);
        CreatedBy = userId;
    }

    public void Update(Guid userId, decimal amount, string currency, DateTime date, string description, Guid categoryId)
    {
        SetAmount(amount);
        SetCurrency(currency);
        SetDate(date);
        SetDescription(description);
        SetCategoryId(categoryId);
        UpdatedAt = DateTime.UtcNow;
        UpdatedBy = userId;
    }

    public void SetUserId(Guid userId)
    {
        if (userId == Guid.Empty)
            throw new ExpenseException("Invalid user id");

        UserId = userId;
    }

    public void SetAmount(decimal amount)
    {
        if (amount <= 0)
            throw new ExpenseException("Amount must be greater than zero");

        Amount = amount;
    }

    public void SetCurrency(string currency)
    {
        if (string.IsNullOrEmpty(currency) || string.IsNullOrWhiteSpace(currency))
            throw new ExpenseException("Currency cannot be empty");

        Currency = currency;
    }

    public void SetDate(DateTime date)
    {
        if (date > DateTime.UtcNow)
            throw new ExpenseException("Date cannot be in the future");

        Date = date;
    }

    public void SetDescription(string description)
    {
        if (string.IsNullOrEmpty(description) || string.IsNullOrWhiteSpace(description))
            throw new ExpenseException("Description cannot be empty");

        Description = description;
    }

    public void SetCategoryId(Guid categoryId)
    {
        if (categoryId == Guid.Empty)
            throw new ExpenseException("Invalid category id");

        CategoryId = categoryId;
    }

    public void SetId(Guid id)
    {
        if (id == Guid.Empty)
            throw new ExpenseException("Invalid expense id");

        Id = id;
    }
}
