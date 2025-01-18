namespace Budget.API.Endpoints.Expenses;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expenses", async (ISender sender, HttpContext httpContext, CancellationToken cancellation) =>
        {
            string id = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "";
            Guid userId = Guid.Parse(id);
            
            var result = await sender.Send(new GetExpensesQuery(userId), cancellation);
            
            return Results.Ok(result);
        });
    }
}
