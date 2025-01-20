namespace Budget.Application.Expenses.Update;

public sealed class UpdateExpenseValidator : AbstractValidator<UpdateExpenseCommand>
{
    public UpdateExpenseValidator()
    {
        RuleFor(x => x.ExpenseId)
            .NotEmpty().WithMessage("Expense id cannot be empty");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id cannot be empty");

        RuleFor(x => x.Amount)
            .NotEmpty().WithMessage("Amount cannot be empty")
            .GreaterThan(0).WithMessage("Amount cannot be a negative value");

        RuleFor(x => x.Currency)
            .NotEmpty().WithMessage("Currency cannot be empty");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date cannot be empty")
            .Must(x => DateTime.TryParse(x, out _)).WithMessage("Invalid date format");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("Category id cannot be empty")
            .Must(x => Guid.TryParse(x, out _)).WithMessage("Invalid category id format");
    }
}
