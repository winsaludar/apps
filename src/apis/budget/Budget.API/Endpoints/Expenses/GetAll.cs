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
        .Produces<ExpenseSummaryDto[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
