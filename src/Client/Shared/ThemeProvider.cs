using MudBlazor;
using ReflectInput.Client.Infrastructure.Preferences;
using ReflectInput.Client.Infrastructure.Theme;

namespace ReflectInput.Client.Shared;

public class ThemeProvider
{
    public ClientPreference? ThemePreference { get; private set; }
    public MudTheme CurrentTheme { get; private set; } = new LightTheme();
    public delegate void ThemePreferenceChangedHandler(MudTheme theme);
    public event ThemePreferenceChangedHandler ThemePreferenceChanged;

    public void Initialize(ClientPreference? clientPreference)
    {
        ThemePreference = clientPreference;

        if (ThemePreference == null)
        {
            ThemePreference = new ClientPreference();
        }

        SetCurrentTheme(ThemePreference);
    }

    public void ChangeThemePreference(ClientPreference themePreference)
    {
        SetCurrentTheme(themePreference);
        //await ClientPreferences.SetPreference(themePreference);
    }

    private void SetCurrentTheme(ClientPreference themePreference)
    {
        CurrentTheme = themePreference.IsDarkMode ? new DarkTheme() : new LightTheme();
        CurrentTheme.Palette.Primary = themePreference.PrimaryColor;
        CurrentTheme.Palette.Secondary = themePreference.SecondaryColor;
        CurrentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.BorderRadius}px";
        CurrentTheme.LayoutProperties.DefaultBorderRadius = $"{themePreference.BorderRadius}px";
        //_rightToLeft = themePreference.IsRTL;
        ThemePreferenceChanged?.Invoke(CurrentTheme);
    }
}