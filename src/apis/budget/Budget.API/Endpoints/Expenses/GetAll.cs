namespace Budget.API.Endpoints.Expenses;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expenses", async (IUserContext userContext, ISender sender, HttpContext httpContext, CancellationToken cancellationToken) =>
        {
            List<ExpenseSummaryDto> expenses = await sender.Send(new GetExpensesQuery(userContext.UserId), cancellationToken);
            return Results.Ok(expenses);
        })
        .WithTags("Expenses")
        .Produces<ExpenseSummaryDto[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
