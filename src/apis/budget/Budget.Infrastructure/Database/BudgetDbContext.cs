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
            ?? throw new ExpenseException($"Invalid category id: {expense.CategoryId}");

        Expenses.Add(expense);
    }

    public async Task UpdateExpense(Expense expense)
    {
        Expense? dbExpense = await Expenses.FirstOrDefaultAsync(x => x.Id == expense.Id && x.UserId == expense.UserId)
            ?? throw new ExpenseException($"Invalid expense id: {expense.Id}", HttpStatusCode.NotFound);

        ExpenseCategory? category = await ExpensesCategories.FirstOrDefaultAsync(x => x.Id == expense.CategoryId)
            ?? throw new ExpenseException($"Invalid category id: {expense.CategoryId}");

        dbExpense.Update(expense.UserId, expense.Amount, expense.Currency, expense.Date, expense.Description, expense.CategoryId);
        Expenses.Update(dbExpense);
    }

    async Task IBudgetDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }
}
