namespace Budget.Application.Expenses.Delete;

public sealed class DeleteExpenseCommandHandler(IExpenseDbContext dbContext) : IRequestHandler<DeleteExpenseCommand, Unit>
{
    public async Task<Unit> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        await dbContext.DeleteExpenseAsync(request.ExpenseId, request.UserId);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
