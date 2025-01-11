namespace Shared.Http.HttpClients;

public abstract class BaseHttpClient(HttpClient httpClient)
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

    protected async Task<ClientResponse> PostAsJsonAsync(string url, object payload)
    {
        return await PostAsJsonAsync<SuccessResponse, ErrorResponse>(url, payload, (response) =>
        {
            return (response is ErrorResponse errorResponse)
                ? new() { IsSuccessful = false, Errors = errorResponse.Details }
                : new() { IsSuccessful = true };
        });
    }

    protected async Task<ClientResponse> PostAsJsonAsync<T>(string url, object payload, Func<BaseResponse, ClientResponse> processResponse)
    {
        return await PostAsJsonAsync<T, ErrorResponse>(url, payload, processResponse);
    }

    protected async Task<ClientResponse> PostAsJsonAsync<T, U>(string url, object payload, Func<BaseResponse, ClientResponse> processResponse)
    {
        try
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(url, payload);
            string responseContent = await response.Content.ReadAsStringAsync();

            dynamic? result = response.IsSuccessStatusCode
                ? JsonSerializer.Deserialize<T>(responseContent, _jsonSerializerOptions)
                : JsonSerializer.Deserialize<U>(responseContent, _jsonSerializerOptions);

            return processResponse(result);
        }
        catch (TaskCanceledException ex)
        {
            // Handle request cancellation or timeouts
            Console.Error.WriteLine($"Request timeout: {ex.Message}");
            return new() { IsSuccessful = false, Errors = ["The request timed out. Please try again later."] };
        }
        catch (Exception ex)
        {
            // Handle any other exceptions
            Console.Error.WriteLine($"Unexpected error: {ex.Message}");
            return new() { IsSuccessful = false, Errors = ["Something went wrong, please try again later."] };
        }
    }
}
