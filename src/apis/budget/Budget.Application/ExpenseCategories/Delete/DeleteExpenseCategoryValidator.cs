namespace Budget.Application.ExpenseCategories.Delete;

public sealed class DeleteExpenseCategoryValidator : AbstractValidator<DeleteExpenseCategoryCommand>
{
    public DeleteExpenseCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Expense category id cannot be empty");
    }
}
