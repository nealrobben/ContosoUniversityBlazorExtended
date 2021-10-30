using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentDetailsViewModel : DepartmentViewModelBase
    {
        private string _id;
        private MudDialogInstance _mudDialog;

        public DepartmentDetailVM Department { get; set; }

        public DepartmentDetailsViewModel(DepartmentService departmentService, 
            IStringLocalizer<DepartmentResources> departmentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(departmentService, departmentLocalizer, generalLocalizer)
        {
        }

        public async Task OnInitializedAsync(MudDialogInstance MudDialog, string id)
        {
            _mudDialog = MudDialog;
            _id = id;
            Department = await _departmentService.GetAsync(_id);
        }

        public void Close()
        {
            _mudDialog.Cancel();
        }
    }
}
