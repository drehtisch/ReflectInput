using Microsoft.AspNetCore.Components;
using MudBlazor;
using ReflectInput.Client.Infrastructure.Preferences;
using ReflectInput.Client.Infrastructure.Theme;

namespace ReflectInput.Client.Shared;

public partial class BaseLayout
{
    private bool _rightToLeft;

    [Inject]
    private ThemeProvider? _themeProvider { get; set; }

    private MudTheme _currentTheme { get; set; } = new LightTheme();

    protected override async Task OnInitializedAsync()
    {
        var clientPreference = await ClientPreferences.GetPreference() as ClientPreference;

        if (_themeProvider is not null)
        {
            _themeProvider.Initialize(clientPreference);
            _themeProvider.ThemePreferenceChanged += ThemePreferenceChanged;
        }
    }

    public void ThemePreferenceChanged(MudTheme theme)
    {
        _currentTheme = theme;
        StateHasChanged();
    }
}