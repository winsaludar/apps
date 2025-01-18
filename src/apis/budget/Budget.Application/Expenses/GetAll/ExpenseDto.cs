namespace Budget.Application.Expenses.GetAll;

public sealed record ExpenseDto(
    Guid Id,
    Guid UserId,
    decimal Amount,
    string Currencry,
    string Description,
    DateTime Date,
    Guid CategoryId,
    Guid CategoryName);
