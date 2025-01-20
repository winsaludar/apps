namespace Budget.Application.ExpenseCategories.GetAll;

public sealed class GetExpenseCategoriesQueryHandler(IExpenseCategoryRepository repository) : IRequestHandler<GetExpenseCategoriesQuery, List<ExpenseCategorySummaryDto>>
{
    public async Task<List<ExpenseCategorySummaryDto>> Handle(GetExpenseCategoriesQuery request, CancellationToken cancellationToken) 
        => await repository.GetAllAsync(cancellationToken);
}
