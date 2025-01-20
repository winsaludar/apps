namespace Budget.Application.ExpenseCategories.Update;

public sealed class UpdateExpenseCategoryValidator : AbstractValidator<UpdateExpenseCategoryCommand>
{
    public UpdateExpenseCategoryValidator()
    {
        RuleFor(x => x.ExpenseCategoryId)
            .NotEmpty().WithMessage("Expense category id cannot be empty");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User id cannot be empty");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description cannot be empty");

        RuleFor(x => x.ParentCategoryId)
            .Must(x => Guid.TryParse(x, out _))
                .When(x => x.ParentCategoryId is not null)
                .WithMessage("Invalid parent category id format");
    }
}
