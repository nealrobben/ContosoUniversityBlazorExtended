using System.Net.Http;

namespace WebUI.Client.Services
{
    public abstract class ServiceBase
    {
        protected const string ApiBase = "/api";
        protected abstract string ControllerName { get; }
        protected string Endpoint => $"{ApiBase}/{ControllerName}";

        protected HttpClient _http;

        public ServiceBase(HttpClient http)
        {
            _http = http;
        }
    }
}
