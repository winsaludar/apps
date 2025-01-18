namespace Budget.API.Endpoints.Expenses;

internal sealed class Get : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expenses", async () =>
        {
            List<object> data = [];
            for (int i = 0; i < 10; i++)
            {
                data.Add(new { Id = i + 1, Name = $"Expense #{i + 1}" });
            }

            return Results.Ok(data);
        });
    }
}
