using Microsoft.JSInterop;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentsViewModel : DepartmentViewModelBase
    {
        private readonly IJSRuntime _jSRuntime;

        public DepartmentsOverviewVM departmentsOverview { get; set; }

        public DepartmentsViewModel(DepartmentService departmentService,
            IJSRuntime jSRuntime) : base(departmentService)
        {
            _jSRuntime = jSRuntime;
        }

        public async Task OnInitializedAsync()
        {
            departmentsOverview = await _departmentService.GetAllAsync();
        }

        public async Task DeleteDepartment(int departmentId, string departmentName)
        {
            if (!await _jSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete the department '{departmentName}'?"))
                return;

            var result = await _departmentService.DeleteAsync(departmentId.ToString());

            if (result.IsSuccessStatusCode)
            {
                departmentsOverview = await _departmentService.GetAllAsync();
            }
        }
    }
}
