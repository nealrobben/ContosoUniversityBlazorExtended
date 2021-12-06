using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace WebUI.Client.Services
{
    public interface IDepartmentService
    {
        Task CreateAsync(CreateDepartmentCommand createCommand);
        Task DeleteAsync(string id);
        Task<DepartmentsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize);
        Task<DepartmentDetailVM> GetAsync(string id);
        Task<DepartmentsLookupVM> GetLookupAsync();
        Task UpdateAsync(UpdateDepartmentCommand createCommand);
    }

    public class DepartmentService : ServiceBase, IDepartmentService
    {
        private const string _departmentsEndoint = $"{apiBase}/departments";

        public DepartmentService(HttpClient http) : base(http)
        {
        }

        public async Task<DepartmentsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = _departmentsEndoint;

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

            return await _http.GetFromJsonAsync<DepartmentsOverviewVM>(url);
        }

        public async Task<DepartmentDetailVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<DepartmentDetailVM>($"{_departmentsEndoint}/{id}");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{_departmentsEndoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateDepartmentCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(_departmentsEndoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateDepartmentCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(_departmentsEndoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<DepartmentsLookupVM> GetLookupAsync()
        {
            return await _http.GetFromJsonAsync<DepartmentsLookupVM>($"{_departmentsEndoint}/lookup");
        }
    }
}
