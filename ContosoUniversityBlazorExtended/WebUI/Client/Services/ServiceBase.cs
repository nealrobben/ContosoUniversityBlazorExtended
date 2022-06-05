using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace WebUI.Client.Services
{
    public abstract class ServiceBase<TOverviewVM>
    {
        protected const string ApiBase = "/api";
        protected abstract string ControllerName { get; }
        protected string Endpoint => $"{ApiBase}/{ControllerName}";

        protected HttpClient _http;

        public ServiceBase(HttpClient http)
        {
            _http = http;
        }

        public async Task<TOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = Endpoint;

            if (pageNumber != null)
            {
                url += $"?pageNumber={pageNumber}";
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                if (!url.Contains("?"))
                    url += $"?searchString={searchString}";
                else
                    url += $"&searchString={searchString}";
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (!url.Contains("?"))
                    url += $"?sortOrder={sortOrder}";
                else
                    url += $"&sortOrder={sortOrder}";
            }

            if (pageSize != null)
            {
                if (!url.Contains("?"))
                    url += $"?pageSize={pageSize}";
                else
                    url += $"&pageSize={pageSize}";
            }

            return await _http.GetFromJsonAsync<TOverviewVM>(url);
        }
    }
}
