using System.Net.Http.Json;
using System.Net;
using Application.Entities.Customers.Commands.CreateCustomerCommand;
using System.Text.Json;
using Application.Entities.Customers.Queries.GetCustomerById;
using Application.Entities.Customers.Commands.DeleteCustomerById;

namespace WebApi.Client.HttpClients;

public class CustomerHttpClient : IDisposable
{
    private readonly HttpClient _client;

    public CustomerHttpClient(string apiBaseAddress)
    {
        _client = new HttpClient()
        {
            BaseAddress = new Uri(apiBaseAddress)
        };
    }

    /// <exception cref="ApiValidationException"/>
    /// <exception cref="ApiBaseException"/>
    public async Task<ApiResult<int>> CreateAsync(CreateCustomerCommand request, CancellationToken cancellationToken = default)
    {
        var res = await _client.PostAsJsonAsync("api/v1/Customers", request, cancellationToken);

        if (!res.IsSuccessStatusCode)
        {
            throw await GetPropperException(res);
        }

        return await res.Content.ReadFromJsonAsync<ApiResult<int>>();
    }

    /// <exception cref="ApiNotFoundException"/>
    /// <exception cref="ApiBaseException"/>
    public async Task<ApiResult<CustomerDto>> GetCustomerByIdAsync(GetCustomerById request, CancellationToken cancellationToken = default)
    {
        var res = await _client.GetAsync($"api/v1/Customers?Id={request.Id}", cancellationToken);

        if (!res.IsSuccessStatusCode)
        {
            throw await GetPropperException(res);
        }

        return await res.Content.ReadFromJsonAsync<ApiResult<CustomerDto>>();
    }

    /// <exception cref="ApiNotFoundException"/>
    /// <exception cref="ApiBaseException"/>
    public async Task<ApiResult<bool>> DeleteCustomerByIdAsync(DeleteCustomerById request, CancellationToken cancellationToken = default)
    {
        var res = await _client.DeleteAsync($"api/v1/Customers?Id={request.Id}", cancellationToken);

        if (!res.IsSuccessStatusCode)
        {
            throw await GetPropperException(res);
        }

        return await res.Content.ReadFromJsonAsync<ApiResult<bool>>();
    }

    private async Task<Exception> GetPropperException(HttpResponseMessage res)
    {
        switch (res.StatusCode)
        {
            case HttpStatusCode.BadRequest:
                {
                    var apiResult = await res.Content.ReadFromJsonAsync<ApiResult<IDictionary<string, string[]>>>();
                    return new ApiValidationException(apiResult);
                }
            case HttpStatusCode.NotFound:
                {
                    var apiResult = await res.Content.ReadFromJsonAsync<ApiResult>();
                    return new ApiNotFoundException(apiResult.Message, apiResult);
                }
            default:
                {
                    var apiResult = await res.Content.ReadFromJsonAsync<ApiResult>();
                    return new ApiBaseException(apiResult.Message, apiResult);
                }
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}