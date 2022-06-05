﻿using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Client.Extensions;

namespace WebUI.Client.Services
{
    public interface IServiceBase<TOverviewVM, TDetailsVM, TCreateCommand, TUpdateCommand>
    {
        Task CreateAsync(TCreateCommand createCommand);
        Task DeleteAsync(string id);
        Task<TOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize);
        Task<TDetailsVM> GetAsync(string id);
        Task UpdateAsync(TUpdateCommand createCommand);
    }

    public abstract class ServiceBase<TOverviewVM, TDetailsVM, TCreateCommand, TUpdateCommand>
    {
        private const string PageNumberParameterName = "pageNumber";
        private const string SearchStringParameterName = "searchString";
        private const string SortOrderParameterName = "sortOrder";
        private const string PageSizeParameterName = "pageSize";

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
            var url = Endpoint.AppendParameter(PageNumberParameterName, pageNumber)
                .AppendParameter(SearchStringParameterName, searchString)
                .AppendParameter(SortOrderParameterName, sortOrder)
                .AppendParameter(PageSizeParameterName, pageSize);

            return await _http.GetFromJsonAsync<TOverviewVM>(url);
        }

        public async Task<TDetailsVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<TDetailsVM>($"{Endpoint}/{id}");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{Endpoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(TCreateCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(Endpoint, createCommand);

            CheckResultForError(result);
        }

        public async Task UpdateAsync(TUpdateCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(Endpoint, createCommand);

            CheckResultForError(result);
        }

        private async void CheckResultForError(HttpResponseMessage result)
        {
            var status = (int)result.StatusCode;

            if (status == 400)
            {
                var responseData_ = result.Content == null ? null : await result.Content.ReadAsStringAsync().ConfigureAwait(false);
                throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData_, null, null);
            }

            result.EnsureSuccessStatusCode();
        }
    }
}
