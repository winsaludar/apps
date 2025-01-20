namespace Budget.Application.ExpenseCategories.Delete;

public sealed class DeleteExpenseCategoryCommandHandler(IExpenseCategoryDbContext dbContext) : IRequestHandler<DeleteExpenseCategoryCommand, Unit>
{
    public async Task<Unit> Handle(DeleteExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        await dbContext.DeleteExpenseCategoryAsync(request.Id);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
