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
    public class DepartmentService : ServiceBase
    {
        public DepartmentService(HttpClient http) : base(http)
        {
        }

        public async Task<DepartmentsOverviewVM> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<DepartmentsOverviewVM>("/api/departments");
        }

        public async Task<DepartmentDetailVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<DepartmentDetailVM>($"/api/departments/{id}");
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _http.DeleteAsync($"/api/departments/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateDepartmentCommand createCommand)
        {
            return await _http.PostAsJsonAsync($"/api/departments", createCommand);
        }

        public async Task<HttpResponseMessage> UpdateAsync(UpdateDepartmentCommand createCommand)
        {
            return await _http.PutAsJsonAsync($"/api/departments", createCommand);
        }

        public async Task<DepartmentsLookupVM> GetLookupAsync()
        {
            return await _http.GetFromJsonAsync<DepartmentsLookupVM>("/api/departments/lookup");
        }
    }
}
