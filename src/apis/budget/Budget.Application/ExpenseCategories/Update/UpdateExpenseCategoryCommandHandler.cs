namespace Budget.Application.ExpenseCategories.Update;

public sealed class UpdateExpenseCategoryCommandHandler(IExpenseCategoryDbContext dbContext) : IRequestHandler<UpdateExpenseCategoryCommand, Unit>
{
    public async Task<Unit> Handle(UpdateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        Guid? parentCategoryId = !string.IsNullOrWhiteSpace(request.ParentCategoryId)
            ? Guid.Parse(request.ParentCategoryId)
            : null;
        ExpenseCategory category = new(request.ExpenseCategoryId, request.Name, request.Description, request.UserId, parentCategoryId);

        await dbContext.UpdateExpenseCategoryAsync(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
