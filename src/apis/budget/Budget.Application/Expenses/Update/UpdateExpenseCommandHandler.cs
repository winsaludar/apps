namespace Budget.Application.Expenses.Update;

public class UpdateExpenseCommandHandler(IExpenseDbContext dbContext) : IRequestHandler<UpdateExpenseCommand, Unit>
{
    public async Task<Unit> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(request.UserId);
        DateTime expenseDate = DateTime.Parse(request.Date).ToUniversalTime();
        Guid categoryId = Guid.Parse(request.CategoryId);
        Expense expense = new(request.ExpenseId, userId, request.Amount, request.Currency, expenseDate, request.Description, categoryId);

        await dbContext.UpdateExpense(expense);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
