using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentViewModelBase
    {
        protected readonly DepartmentService _departmentService;

        public DepartmentViewModelBase(DepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
    }
}
