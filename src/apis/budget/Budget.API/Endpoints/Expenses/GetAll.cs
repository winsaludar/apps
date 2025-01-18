namespace Budget.API.Endpoints.Expenses;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expenses", async (IUserContext userContext, ISender sender, HttpContext httpContext, CancellationToken cancellation) =>
        {
            List<ExpenseSummaryDto> expenses = await sender.Send(new GetExpensesQuery(userContext.UserId), cancellation);
            return Results.Ok(expenses);
        })
        .WithName("GET Expenses")
        .Produces<ExpenseSummaryDto[]>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
