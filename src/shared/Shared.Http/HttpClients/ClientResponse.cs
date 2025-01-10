namespace Shared.Http.HttpClients;

public record ClientResponse<T>
{
    public bool IsSuccessful { get; init; } = default!;
    public IEnumerable<string> Errors { get; init; } = default!;

    public T? Data { get; init; }
}
