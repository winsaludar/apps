namespace Budget.Domain.Expenses;

public sealed class ExpenseException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) : Exception(message) 
{
    public HttpStatusCode StatusCode { get; private set; } = statusCode;
}
