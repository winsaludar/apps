namespace Budget.API.Endpoints.ExpenseCategories;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expense-categories/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) => 
        {
            ExpenseCategoryDetailDto expense = await sender.Send(new GetExpenseCategoryByIdQuery(id), cancellationToken);
            return Results.Ok(expense);
        })
        .WithTags("Expense Categories")
        .WithName("GetExpenseCategoryById")
        .Produces<ExpenseCategoryDetailDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
