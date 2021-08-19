using System.Net.Http;

namespace WebUI.Client.Services
{
    public class ServiceBase
    {
        protected HttpClient _http;

        public ServiceBase(HttpClient http)
        {
            _http = http;
        }
    }
}
