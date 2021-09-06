using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Pages.Departments;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentsViewModel : DepartmentViewModelBase
    {
        private IDialogService _dialogService { get; set; }
        private ISnackbar _snackbar { get; set; }

        public DepartmentsOverviewVM departmentsOverview { get; set; }

        public DepartmentsViewModel(DepartmentService departmentService,
            IDialogService dialogService, ISnackbar snackbar) : base(departmentService)
        {
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        public async Task OnInitializedAsync()
        {
            departmentsOverview = await _departmentService.GetAllAsync();
        }

        public async Task DeleteDepartment(int departmentId, string departmentName)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox("Confirm", $"Are you sure you want to delete the department '{departmentName}'?", 
                yesText: "Delete", cancelText: "Cancel");

            if (dialogResult == true)
            {
                var result = await _departmentService.DeleteAsync(departmentId.ToString());

                if (result.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Deleted department {departmentName}", Severity.Success);
                    departmentsOverview = await _departmentService.GetAllAsync();
                }
            }
        }

        public void OpenDepartmentDetails(int departmentId)
        {
            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", departmentId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall};

            _dialogService.Show<DepartmentDetails>("Department Details", parameters, options);
        }

        public async Task OpenDepartmentEdit(int departmentId)
        {
            var parameters = new DialogParameters();
            parameters.Add("DepartmentId", departmentId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall };

            var dialog = _dialogService.Show<DepartmentEdit>("Department Edit", parameters, options);

            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                departmentsOverview = await _departmentService.GetAllAsync();
            }
        }

        public async Task OpenDepartmentCreate()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog =_dialogService.Show<DepartmentCreate>("Create department", options);
            var result = await dialog.Result;

            if(result.Data != null && (bool)result.Data)
            {
                departmentsOverview = await _departmentService.GetAllAsync();
            }
        }
    }
}
