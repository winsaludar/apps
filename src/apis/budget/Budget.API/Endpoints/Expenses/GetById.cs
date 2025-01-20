namespace Budget.API.Endpoints.Expenses;

internal sealed class GetById : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("expenses/{id:guid}", async (Guid id, IUserContext userContext, ISender sender) =>
        {
            ExpenseDetailDto expense = await sender.Send(new GetExpenseByIdQuery(userContext.UserId, id));
            return Results.Ok(expense);
        })
        .WithTags("Expenses")
        .WithName("GetExpenseById")
        .Produces<ExpenseSummaryDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces<ErrorResponse>(StatusCodes.Status404NotFound)
        .Produces<ErrorResponse>(StatusCodes.Status500InternalServerError);
    }
}
