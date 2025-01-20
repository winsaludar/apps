namespace Budget.Application.ExpenseCategories.Create;

public sealed class CreateExpenseCategoryCommandHandler(IExpenseCategoryDbContext dbContext) : IRequestHandler<CreateExpenseCategoryCommand, Guid>
{
    public async Task<Guid> Handle(CreateExpenseCategoryCommand request, CancellationToken cancellationToken)
    {
        Guid? parentCategoryId = !string.IsNullOrWhiteSpace(request.ParentCategoryId)
            ? Guid.Parse(request.ParentCategoryId)
            : null;
        ExpenseCategory category = new(Guid.NewGuid(), request.Name, request.Description, request.UserId, parentCategoryId);

        await dbContext.AddExpenseCategoryAsync(category);
        await dbContext.SaveChangesAsync(cancellationToken);

        return category.Id;
    }
}
