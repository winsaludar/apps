namespace Budget.API.Endpoints.ExpenseCategories;

internal sealed class Update : IEndpoint
{
    public sealed record UpdateExpenseCategoryRequest(Guid ExpenseCategoryId, string Name, string Description, string? ParentCategoryId = null);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("expense-categories/{id:guid}", async (Guid id, UpdateExpenseCategoryRequest request, IUserContext userContext, ISender sender, CancellationToken cancellationToken) => 
        {
            UpdateExpenseCategoryCommand command = new(id, userContext.UserId, request.Name, request.Description, request.ParentCategoryId);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        })
        .WithTags("Expense Categories")
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
