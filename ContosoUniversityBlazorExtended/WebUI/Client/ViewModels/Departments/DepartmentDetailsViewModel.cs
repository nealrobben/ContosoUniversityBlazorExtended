using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentDetailsViewModel : DepartmentViewModelBase
    {
        private string _id;

        public DepartmentDetailVM Department { get; set; }

        public DepartmentDetailsViewModel(DepartmentService departmentService)
            : base(departmentService)
        {
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;
            Department = await _departmentService.GetAsync(id);
        }
    }
}
