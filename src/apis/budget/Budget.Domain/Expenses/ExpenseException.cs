namespace Budget.Domain.Expenses;

public sealed class ExpenseException(string message) : Exception(message) { }
