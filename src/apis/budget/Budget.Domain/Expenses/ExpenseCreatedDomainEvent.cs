namespace Budget.Domain.Expenses;

public sealed class ExpenseCreatedDomainEvent : IDomainEvent
{
    public Guid ExpenseId { get; }
    public decimal Amount { get; }
    public Guid CategoryId { get; }

    public ExpenseCreatedDomainEvent(Guid expenseId,  decimal amount, Guid categoryId)
    {
        ExpenseId = expenseId;
        Amount = amount;
        CategoryId = categoryId;
    }
}
