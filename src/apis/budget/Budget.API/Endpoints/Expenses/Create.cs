namespace Budget.API.Endpoints.Expenses;

internal sealed class Create : IEndpoint
{
    public sealed record CreateExpenseRequest(decimal Amount, string Currency, string Date, string Description, string CategoryId);

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("expenses", async (CreateExpenseRequest request, IUserContext userContext, ISender sender, CancellationToken cancelationToken) =>
        {
            CreateExpenseCommand command = new(userContext.UserId.ToString(), request.Amount, request.Currency, request.Date, request.Description, request.CategoryId);
            await sender.Send(command, cancelationToken);
            return Results.Created();
        })
        .Produces<SuccessResponse>(StatusCodes.Status201Created)
        .Produces<ErrorResponse>(StatusCodes.Status400BadRequest)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
