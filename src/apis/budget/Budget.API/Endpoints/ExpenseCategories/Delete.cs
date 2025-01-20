namespace Budget.API.Endpoints.ExpenseCategories;

internal sealed class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("expense-categories/{id:guid}", async (Guid id, ISender sender, CancellationToken cancellationToken) => 
        {
            DeleteExpenseCategoryCommand command = new(id);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        })
        .WithTags("Expense Categories")
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
