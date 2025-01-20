namespace Budget.Infrastructure.Database;

public sealed class BudgetDbContext(DbContextOptions<BudgetDbContext> options) : DbContext(options), IExpenseDbContext, IExpenseCategoryDbContext
{
    public DbSet<Expense> Expenses { get; set; }
    public DbSet<ExpenseCategory> ExpenseCategories { get; set;}

    #region EXPENSES

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(typeof(BudgetDbContext).Assembly);
    }

    public async Task AddExpenseAsync(Expense expense) 
    {
        ExpenseCategory? category = await ExpenseCategories.FirstOrDefaultAsync(x => x.Id == expense.CategoryId) 
            ?? throw new ExpenseException($"Invalid category id: {expense.CategoryId}");

        Expenses.Add(expense);
    }

    public async Task UpdateExpenseAsync(Expense expense)
    {
        Expense? dbExpense = await Expenses.FirstOrDefaultAsync(x => x.Id == expense.Id && x.UserId == expense.UserId)
            ?? throw new ExpenseException($"Invalid expense id: {expense.Id}", HttpStatusCode.NotFound);

        ExpenseCategory? category = await ExpenseCategories.FirstOrDefaultAsync(x => x.Id == expense.CategoryId)
            ?? throw new ExpenseException($"Invalid category id: {expense.CategoryId}");

        dbExpense.Update(expense.UserId, expense.Amount, expense.Currency, expense.Date, expense.Description, expense.CategoryId);
        Expenses.Update(dbExpense);
    }

    public async Task DeleteExpenseAsync(Guid expenseId, Guid userId)
    {
        Expense? dbExpense = await Expenses.FirstOrDefaultAsync(x => x.Id == expenseId && x.UserId == userId)
            ?? throw new ExpenseException($"Invalid expense id: {expenseId}", HttpStatusCode.NotFound);

        Expenses.Remove(dbExpense);
    }

    async Task IExpenseDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    #endregion

    #region EXPENSE_CATEGORIES

    public async Task AddExpenseCategoryAsync(ExpenseCategory expenseCategory)
    {
        // Prevent duplicate category name
        if (await ExpenseCategories.AnyAsync(x => x.Name.ToLower() == expenseCategory.Name.ToLower()))
            throw new ExpenseException($"Category with name: {expenseCategory.Name} already exist");

        // Target parent category does not exist
        if (expenseCategory.ParentCategoryId is not null)
        {
            _ = await ExpenseCategories.FirstOrDefaultAsync(x => x.Id == expenseCategory.ParentCategoryId)
                ?? throw new ExpenseException($"Invalid parent category id: {expenseCategory.ParentCategoryId}");
        }

        ExpenseCategories.Add(expenseCategory);
    }

    public async Task UpdateExpenseCategoryAsync(ExpenseCategory expenseCategory)
    {
        // Target category does not exist
        ExpenseCategory? dbCategory = await ExpenseCategories.FirstOrDefaultAsync(x => x.Id == expenseCategory.Id)
            ?? throw new ExpenseException($"Invalid expense category id: {expenseCategory.Id}");

        // Different category with new updated name already exist
        if (await ExpenseCategories.AnyAsync(x => x.Id != expenseCategory.Id &&  x.Name.ToLower() == expenseCategory.Name.ToLower()))
            throw new ExpenseException($"Category with name: {expenseCategory.Name} already exist");

        // Target parent category does not exist
        if (expenseCategory.ParentCategoryId is not null)
        {
            _ = await ExpenseCategories.FirstOrDefaultAsync(x => x.Id == expenseCategory.ParentCategoryId)
                ?? throw new ExpenseException($"Invalid parent category id: {expenseCategory.ParentCategoryId}");
        }

        dbCategory.Update(expenseCategory.Name, expenseCategory.Description, expenseCategory.CreatedBy, expenseCategory.ParentCategoryId);
        ExpenseCategories.Update(dbCategory);
    }

    async Task IExpenseCategoryDbContext.SaveChangesAsync(CancellationToken cancellationToken)
    {
        await SaveChangesAsync(cancellationToken);
    }

    #endregion
}
