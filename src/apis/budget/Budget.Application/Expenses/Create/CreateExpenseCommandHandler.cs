namespace Budget.Application.Expenses.Create;

public sealed class CreateExpenseCommandHandler(IExpenseDbContext dbContext) : IRequestHandler<CreateExpenseCommand, Guid>
{
    public async Task<Guid> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        DateTime expenseDate = DateTime.Parse(request.Date).ToUniversalTime();
        Guid categoryId = Guid.Parse(request.CategoryId);
        Expense newExpense = new(Guid.NewGuid(), request.UserId, request.Amount, request.Currency, expenseDate, request.Description, categoryId);

        await dbContext.AddExpenseAsync(newExpense);
        await dbContext.SaveChangesAsync(cancellationToken);

        return newExpense.Id;
    }
}
