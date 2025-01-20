namespace Budget.API.Endpoints.Expenses;

internal sealed class Update : IEndpoint
{
    public sealed record UpdateExpenseRequest(decimal Amount, string Currency, string Date, string Description, string CategoryId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("expenses/{id:guid}", async (Guid id, UpdateExpenseRequest request, IUserContext userContext, ISender sender, CancellationToken cancellationToken) => 
        {
            UpdateExpenseCommand command = new(id, userContext.UserId.ToString(), request.Amount, request.Currency, request.Date, request.Description, request.CategoryId);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
