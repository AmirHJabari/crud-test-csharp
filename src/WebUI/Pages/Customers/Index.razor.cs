using Microsoft.AspNetCore.Components.Web;
using Application.Entities.Customers.Queries.GetCustomersWithPagination;
using Application.Common.Models;

namespace WebUI.Pages.Customers;

public partial class Index : ComponentBase
{
    public MudTable<CustomerPaginationDto> customersTable = new();
    private PaginatedList<CustomerPaginationDto> Customers { get; set; }

    [Inject]
    public ILocalStorageService LocalStorage { get; set; }
    [Inject]
    public ISnackbar Snackbar { get; set; }
    [Inject]
    public CustomerHttpClient ApiClient { get; set; }
    [Inject]
    public IDialogService DialogService { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        var error = await LocalStorage.GetItemAsStringAsync("ShowErrorMessage");
        if (!string.IsNullOrWhiteSpace(error))
        {
            await LocalStorage.RemoveItemAsync("ShowErrorMessage");
            Snackbar.Add(error, Severity.Error);
        }
    }

    /// <summary>
    /// Here we simulate getting the paged, filtered and ordered data from the server
    /// </summary>
    private async Task<TableData<CustomerPaginationDto>> ReloadCustomersTableData(TableState state)
    {
        var apiResult = await ApiClient.GetCustomersWithPaginationAsync(new()
        {
            PageNumber = state.Page + 1,
            PageSize = state.PageSize
        });
        Customers = apiResult.Data;

        this.StateHasChanged();

        return new TableData<CustomerPaginationDto>() { TotalItems = Customers.TotalCount, Items = Customers.Items };
    }

    async Task DeleteItemClicked(int id, string firstName)
    {
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = await DialogService.ShowAsync<DeleteDialog>($"Delete {firstName}", options);
        var result = await dialog.Result;
        if (!result.Canceled)
        {
            var apiResult = await ApiClient.DeleteCustomerByIdAsync(new() { Id = id });
            if (apiResult.IsSuccess)
            {
                await customersTable.ReloadServerData();
                Snackbar.Add("Customer deleted!", Severity.Success);
            }
        }
    }
}