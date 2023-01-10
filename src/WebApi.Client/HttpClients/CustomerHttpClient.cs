using System.Net.Http.Json;
using System.Net;
using Application.Entities.Customers.Commands.CreateCustomerCommand;
using System.Text.Json;
using Application.Entities.Customers.Queries.GetCustomerById;
using Application.Entities.Customers.Commands.DeleteCustomerById;
using Application.Entities.Customers.Queries.GetCustomersWithPagination;
using Application.Common.Models;
using Application.Entities.Customers.Commands.EditCustomerCommand;

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

        return await res.Content.ReadFromJsonAsync<ApiResult<int>>(cancellationToken: cancellationToken);
    }

    /// <exception cref="ApiNotFoundException"/>
    /// <exception cref="ApiBaseException"/>
    public async Task<ApiResult<CustomerDto>> GetCustomerByIdAsync(GetCustomerById request, CancellationToken cancellationToken = default)
    {
        var res = await _client.GetAsync($"api/v1/Customers/{request.Id}", cancellationToken);

        if (!res.IsSuccessStatusCode)
        {
            throw await GetPropperException(res);
        }

        return await res.Content.ReadFromJsonAsync<ApiResult<CustomerDto>>(cancellationToken: cancellationToken);
    }

    /// <exception cref="ApiBaseException"/>
    public async Task<ApiResult<PaginatedList<CustomerPaginationDto>>> GetCustomersWithPaginationAsync(GetCustomersWithPagination request, CancellationToken cancellationToken = default)
    {
        var res = await _client.GetAsync(
                $"api/v1/Customers?PageNumber={request.PageNumber}&PageSize={request.PageSize}",
                cancellationToken: cancellationToken);

        if (!res.IsSuccessStatusCode)
        {
            throw await GetPropperException(res);
        }

        return await res.Content.ReadFromJsonAsync<ApiResult<PaginatedList<CustomerPaginationDto>>>(cancellationToken: cancellationToken);
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

        return await res.Content.ReadFromJsonAsync<ApiResult<bool>>(cancellationToken: cancellationToken);
    }

    /// <exception cref="ApiValidationException"/>
    /// <exception cref="ApiNotFoundException"/>
    /// <exception cref="ApiBaseException"/>
    public async Task<ApiResult<bool>> EditCustomerAsync(EditCustomerCommand request, CancellationToken cancellationToken = default)
    {
        var res = await _client.PutAsJsonAsync($"api/v1/Customers", request, cancellationToken);

        if (!res.IsSuccessStatusCode)
        {
            throw await GetPropperException(res);
        }

        return await res.Content.ReadFromJsonAsync<ApiResult<bool>>(cancellationToken: cancellationToken);
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