namespace Budget.Application.Expenses.GetAll;

public sealed record ExpenseSummaryDto(
    Guid Id,
    Guid UserId,
    decimal Amount,
    string Currencry,
    string Description,
    DateTime Date,
    Guid CategoryId,
    string CategoryName);
