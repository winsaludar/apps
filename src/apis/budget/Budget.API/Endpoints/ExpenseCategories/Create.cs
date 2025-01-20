namespace Budget.API.Endpoints.ExpenseCategories;

public class Create : IEndpoint
{
    public sealed record CreateExpenseCategoryRequest(string Name, string Description, string? ParentCategoryId = null);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("expense-categories", async (CreateExpenseCategoryRequest request, IUserContext userContext, ISender sender, LinkGenerator linkGenerator, CancellationToken cancellationToken) =>
        {
            CreateExpenseCategoryCommand command = new(userContext.UserId, request.Name, request.Description, request.ParentCategoryId);
            Guid newId = await sender.Send(command, cancellationToken);

            var result = new { id = newId };
            string? locationUrl = linkGenerator.GetPathByName("GetExpenseCategoryById", result);

            return Results.Created(locationUrl, result);
        })
        .WithTags("Expense Categories")
        .Produces<SuccessResponse>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
