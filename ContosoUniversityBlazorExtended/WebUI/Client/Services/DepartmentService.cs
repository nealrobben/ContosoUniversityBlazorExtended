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
        : IServiceBase<DepartmentsOverviewVM, DepartmentDetailVM,
            CreateDepartmentCommand, UpdateDepartmentCommand>
    {
        Task<DepartmentsLookupVM> GetLookupAsync();
    }

    public class DepartmentService 
        : ServiceBase<DepartmentsOverviewVM, DepartmentDetailVM, 
            CreateDepartmentCommand, UpdateDepartmentCommand>, IDepartmentService
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
