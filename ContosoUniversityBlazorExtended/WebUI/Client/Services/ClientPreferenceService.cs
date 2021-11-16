using Microsoft.JSInterop;
using System;

namespace WebUI.Client.Services
{
    public class ClientPreferenceService
    {
        private IJSRuntime _jsRunTime;

        public ClientPreferenceService(IJSRuntime jsRuntime)
        {
            _jsRunTime = jsRuntime;
        }

        public void SetCulture(string name)
        {
            var js = (IJSInProcessRuntime)_jsRunTime;
            js.InvokeVoid("blazorCulture.set", name);
        }

        public bool ToggleDarkMode()
        {
            var js = (IJSInProcessRuntime)_jsRunTime;
            var darkModeValue = js.Invoke<string>("blazorDarkMode.get");
            var darkMode = string.Equals(darkModeValue, "true") ? true : false;

            js.InvokeVoid("blazorDarkMode.set", !darkMode);

            return !darkMode;
        }
    }
}
