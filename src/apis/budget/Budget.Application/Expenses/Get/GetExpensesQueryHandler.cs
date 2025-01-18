namespace Budget.Application.Expenses.Get;

public sealed class GetExpensesQueryHandler : IRequestHandler<GetExpensesQuery, List<Expense>>
{
    public async Task<List<Expense>> Handle(GetExpensesQuery request, CancellationToken cancellationToken)
    {
        // TODO: Return from db
        
        List<Expense> data = [];
        Random rand = new();
        ExpenseCategory category = new(request.UserId, "Foods", "Foods", null);

        for (int i = 0; i < 10; i++)
        {
            decimal amount = rand.Next(1000, 5000);
            data.Add(new(request.UserId, amount, "PHP", DateTime.UtcNow, category, $"Sample Expense #{i + 1}"));
        }

        return data;
    }
}
