using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReflectInput.Client.Infrastructure.Preferences;
using ReflectInput.Client.Infrastructure.Theme;

namespace ReflectInput.Client.Shared;

public partial class BaseLayout
{
    [Inject]
    private ThemeProvider? _themeProvider { get; set; }

    private MudTheme _currentTheme { get; set; } = new LightTheme();

    protected override async Task OnInitializedAsync()
    {
        var clientPreference = await ClientPreferences.GetPreference() as ClientPreference;

        if (_themeProvider is not null)
        {
            _themeProvider.ThemePreferenceChanged += ThemePreferenceChanged;
            _themeProvider.Initialize(clientPreference);
        }
    }

    public void ThemePreferenceChanged(MudTheme theme)
    {
        _currentTheme = theme;
        StateHasChanged();
    }
}