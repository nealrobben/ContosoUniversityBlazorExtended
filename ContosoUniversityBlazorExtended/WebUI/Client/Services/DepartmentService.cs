using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;
using WebUI.Client.Extensions;

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

    public class DepartmentService : ServiceBase<DepartmentsOverviewVM, DepartmentDetailVM, CreateDepartmentCommand, UpdateDepartmentCommand>, IDepartmentService
    {
        protected override string ControllerName => "departments";

        public DepartmentService(HttpClient http) : base(http)
        {
        }

        public async Task<DepartmentsLookupVM> GetLookupAsync()
        {
            return await _http.GetFromJsonAsync<DepartmentsLookupVM>($"{Endpoint}/lookup");
        }
    }
}
