using Blazored.LocalStorage;
using Microsoft.JSInterop;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebUI.Client.Settings;

namespace WebUI.Client.Services
{
    public class ClientSettingService
    {
        private const string clientSettingsKey = "ClientSettings";

        private IJSRuntime _jsRunTime;
        private readonly ILocalStorageService _localStorageService;

        public ClientSettingService(IJSRuntime jsRuntime, ILocalStorageService localStorageService)
        {
            _jsRunTime = jsRuntime;
            _localStorageService = localStorageService;
        }

        public void SetCulture(string name)
        {
            var js = (IJSInProcessRuntime)_jsRunTime;
            js.InvokeVoid("blazorCulture.set", name);
        }

        public async Task<bool> ToggleDarkModeAsync()
        {
            var preference = await GetSettings();
            if (preference != null)
            {
                preference.IsDarkMode = !preference.IsDarkMode;
                await SetPreference(preference);
                return !preference.IsDarkMode;
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

        public async Task SetPreference(ClientSetting setting)
        {
            await _localStorageService.SetItemAsync(clientSettingsKey, setting);
        }
    }
}
