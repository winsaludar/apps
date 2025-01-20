namespace Budget.API.Endpoints.Expenses;

internal sealed class Create : IEndpoint
{
    public sealed record CreateExpenseRequest(decimal Amount, string Currency, string Date, string Description, string CategoryId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("expenses", async (CreateExpenseRequest request, HttpContext httpContext, LinkGenerator linkGenerator, IUserContext userContext, ISender sender, CancellationToken cancelationToken) =>
        {
            CreateExpenseCommand command = new(userContext.UserId, request.Amount, request.Currency, request.Date, request.Description, request.CategoryId);
            Guid newId = await sender.Send(command, cancelationToken);

            var result = new { id = newId };
            string? locationUrl = linkGenerator.GetPathByName("GetItemById", result);

            return Results.Created(locationUrl, result);
        })
        .WithTags("Expenses")
        .Produces<SuccessResponse>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
