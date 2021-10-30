using Microsoft.Extensions.Localization;
using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentViewModelBase
    {
        protected readonly DepartmentService _departmentService;

        protected readonly IStringLocalizer<DepartmentResources> _departmentLocalizer;
        protected readonly IStringLocalizer<GeneralResources> _generalLocalizer;

        public DepartmentViewModelBase(DepartmentService departmentService, IStringLocalizer<DepartmentResources> departmentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
        {
            _departmentService = departmentService;
            _departmentLocalizer = departmentLocalizer;
            _generalLocalizer = generalLocalizer;
        }
    }
}
