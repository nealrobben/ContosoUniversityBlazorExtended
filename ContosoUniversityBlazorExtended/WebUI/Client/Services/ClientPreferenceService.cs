using Microsoft.JSInterop;

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
            var isDarkMode = js.Invoke<string>("blazorDarkMode.get");
            //js.InvokeVoid("blazorDarkMode.set", !isDarkMode);

            return false;
            //return !isDarkMode;
        }
    }
}
