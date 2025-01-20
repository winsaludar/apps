namespace Budget.Application.ExpenseCategories.GetById;

public sealed class GetExpenseCategoryByIdQueryHandler(IExpenseCategoryRepository repository) : IRequestHandler<GetExpenseCategoryByIdQuery, ExpenseCategoryDetailDto>
{
    public async Task<ExpenseCategoryDetailDto> Handle(GetExpenseCategoryByIdQuery request, CancellationToken cancellationToken) => 
        await repository.GetByIdAsync(request.Id, cancellationToken) ?? throw new NotFoundException($"Expense Category with id: {request.Id} not found");
}
