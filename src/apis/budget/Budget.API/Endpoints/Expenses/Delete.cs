namespace Budget.API.Endpoints.Expenses;

public class Delete : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("expenses/{id:guid}", async (Guid id, IUserContext userContext, ISender sender, CancellationToken cancellationToken) => 
        {
            DeleteExpenseCommand command = new(id, userContext.UserId);
            await sender.Send(command, cancellationToken);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
