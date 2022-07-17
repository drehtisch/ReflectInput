using Microsoft.AspNetCore.Components;
using ReflectInput.Client.Infrastructure.Preferences;
using ReflectInput.Client.Shared;

namespace ReflectInput.Client.Components.ThemeManager;

public partial class ThemeToggle
{
    private bool _isDarkMode;

    [Inject]
    private ThemeProvider? _themeProvider { get; set; }

    [EditorRequired] private ClientPreference ThemePreference { get; set; } = default!;

    protected override void OnInitialized()
    {
        if (_themeProvider is not null)
        {
            ThemePreference = _themeProvider!.ThemePreference!;
            _isDarkMode = _themeProvider.ThemePreference!.IsDarkMode;
        }
    }

    [Parameter]
    public EventCallback<bool> OnIconClicked { get; set; }

    private async Task ToggleDarkMode()
    {
        _isDarkMode = !_isDarkMode;
        _themeProvider!.ThemePreference!.IsDarkMode = _isDarkMode;
        _themeProvider.ChangeThemePreference(_themeProvider.ThemePreference);
        await OnIconClicked.InvokeAsync(_isDarkMode);
    }
}