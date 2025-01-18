namespace Budget.API.Endpoints.Expenses;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        List<object> data = [];
        for (int i = 0; i < 10; i++)
        {
            data.Add(new { Id = i + 1, Name = $"Expense #{i + 1}" });
        }

        app.MapGet("expenses", async () =>
        {
            return Results.Ok(data);
        });
    }
}
