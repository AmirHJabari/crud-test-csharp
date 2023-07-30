using System.ComponentModel;

namespace WebUI.Shared;

public partial class MainLayout : LayoutComponentBase
{
    [Inject]
    public ILocalStorageService LocalStorage { get; set; }

    bool _drawerOpen = true;
    bool _isDarkMode = true;

    void DrawerToggle()
    {
        _drawerOpen = !_drawerOpen;
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender && _isDarkMode)
        {
            _isDarkMode = await LocalStorage.GetItemAsync<bool?>("isDarkMode") ?? true;
            StateHasChanged();
        }
        
        await base.OnAfterRenderAsync(firstRender);
    }

    async Task OnThemeToggled(bool isDarkMode)
    {
        await LocalStorage.SetItemAsync("isDarkMode", isDarkMode);
    }
}