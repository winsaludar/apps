namespace Budget.Application.Expenses.Delete;

public sealed class DeleteExpenseValidator : AbstractValidator<DeleteExpenseCommand>
{
    public DeleteExpenseValidator()
    {
        RuleFor(x => x.ExpenseId)
            .NotEmpty()
            .WithMessage("Expense id cannot be empty");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("User id cannot be empty");
    }
}
