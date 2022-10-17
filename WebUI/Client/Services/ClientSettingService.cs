using Blazored.LocalStorage;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Settings;

namespace WebUI.Client.Services;

public class ClientSettingService
{
    private const string clientSettingsKey = "ClientSettings";

    private readonly ILocalStorageService _localStorageService;

    public ClientSettingService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task SetCulture(string languageCode)
    {
        var setting = await GetSettings();
        if (setting != null)
        {
            setting.LanguageCode = languageCode;
            await SetSettings(setting);
        }
    }

    public async Task<bool> ToggleDarkModeAsync()
    {
        var setting = await GetSettings();
        if (setting != null)
        {
            setting.IsDarkMode = !setting.IsDarkMode;
            await SetSettings(setting);
            return !setting.IsDarkMode;
        }

        return false;
    }

    public async Task<MudTheme> GetCurrentThemeAsync()
    {
        var setting = await GetSettings();

        if (setting != null)
        {
            if (setting.IsDarkMode == true) return Themes.DarkTheme;
        }

        return Themes.DefaultTheme;
    }

    public async Task<ClientSetting> GetSettings()
    {
        return await _localStorageService.GetItemAsync<ClientSetting>(clientSettingsKey) ?? new ClientSetting();
    }

    public async Task SetSettings(ClientSetting setting)
    {
        await _localStorageService.SetItemAsync(clientSettingsKey, setting);
    }
}
