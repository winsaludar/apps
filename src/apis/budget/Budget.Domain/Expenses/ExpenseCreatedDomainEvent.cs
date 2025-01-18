namespace Budget.Domain.Expenses;

public sealed class ExpenseCreatedDomainEvent(Guid expenseId, decimal amount, Guid categoryId) : IDomainEvent
{
    public Guid ExpenseId { get; } = expenseId;
    public decimal Amount { get; } = amount;
    public Guid CategoryId { get; } = categoryId;
}
