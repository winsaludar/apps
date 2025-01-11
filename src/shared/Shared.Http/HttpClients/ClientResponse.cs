namespace Shared.Http.HttpClients;

public record ClientResponse
{
    public bool IsSuccessful { get; init; } = default!;
    public IEnumerable<string> Errors { get; init; } = default!;
}

public record ClientResponse<T> : ClientResponse
{
    public T? Data { get; init; }
}