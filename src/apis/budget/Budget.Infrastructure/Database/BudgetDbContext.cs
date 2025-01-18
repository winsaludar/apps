using Budget.Application.Expenses.Create;

namespace Budget.Infrastructure.Database;

public sealed class BudgetDbContext(DbContextOptions<BudgetDbContext> options) : DbContext(options), IBudgetDbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpensesCategories { get; set;}

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(BudgetDbContext).Assembly);
    }

    public async Task AddExpense(Expense expense) 
    {
        ExpenseCategory? category = await ExpensesCategories.FirstOrDefaultAsync(x => x.Id == expense.CategoryId) 
            ?? throw new CreateExpenseException($"Invalid category id: {expense.CategoryId}");

        Expenses.Add(expense);
    }

    async Task IBudgetDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }
}
