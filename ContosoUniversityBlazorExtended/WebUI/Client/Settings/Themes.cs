using MudBlazor;

namespace WebUI.Client.Settings
{
    public static class Themes
    {
        public static MudTheme DefaultTheme = new MudTheme()
        {
            Palette = new Palette()
            {
                Primary = "#1E88E5",
                AppbarBackground = "#1E88E5",
                Background = Colors.Grey.Lighten5,
                DrawerBackground = "#FFF",
                DrawerText = "rgba(0,0,0, 0.7)",
                Success = "#007E33"
            }
        };

        public static MudTheme DarkTheme = new MudTheme()
        {
            Palette = new Palette()
            {
                Primary = "#1E88E5",
                Success = "#007E33",
                Black = "#27272f",
                Background = "#32333d",
                BackgroundGrey = "#27272f",
                Surface = "#373740",
                DrawerBackground = "#27272f",
                DrawerText = "rgba(255,255,255, 0.50)",
                AppbarBackground = "#373740",
                AppbarText = "rgba(255,255,255, 0.70)",
                TextPrimary = "rgba(255,255,255, 0.70)",
                TextSecondary = "rgba(255,255,255, 0.50)",
                ActionDefault = "#adadb1",
                ActionDisabled = "rgba(255,255,255, 0.26)",
                ActionDisabledBackground = "rgba(255,255,255, 0.12)",
                DrawerIcon = "rgba(255,255,255, 0.50)"
            }
        };
    }
}
