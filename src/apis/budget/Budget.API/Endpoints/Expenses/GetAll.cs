namespace Budget.API.Endpoints.Expenses;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expenses", async (IUserContext userContext, ISender sender, HttpContext httpContext, CancellationToken cancellation) =>
        {
            var result = await sender.Send(new GetExpensesQuery(userContext.UserId), cancellation);            
            return Results.Ok(result);
        });
    }
}
