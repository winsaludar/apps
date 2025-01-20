namespace Budget.API.Endpoints.ExpenseCategories;

internal sealed class GetAll : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expense-categories", async (ISender sender, CancellationToken cancellationToken) => 
        {
            GetExpenseCategoriesQuery query = new();
            List<ExpenseCategorySummaryDto> categories = await sender.Send(query, cancellationToken); 
            return Results.Ok(categories);
        })
        .WithTags("Expense Categories")
        .Produces<ExpenseCategorySummaryDto[]>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
