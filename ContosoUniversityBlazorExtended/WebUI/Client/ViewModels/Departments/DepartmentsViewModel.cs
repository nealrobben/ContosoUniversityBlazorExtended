using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Pages.Departments;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentsViewModel : DepartmentViewModelBase
    {
        private IDialogService _dialogService { get; set; }
        private ISnackbar _snackbar { get; set; }

        public MudTable<DepartmentVM> Table { get; set; }

        public DepartmentsOverviewVM DepartmentsOverview { get; set; } = new DepartmentsOverviewVM();

        public DepartmentsViewModel(DepartmentService departmentService,
            IDialogService dialogService, ISnackbar snackbar) : base(departmentService)
        {
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        private async Task GetDepartments()
        {
            await Table.ReloadServerData();
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
                    await GetDepartments();
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
                await GetDepartments();
            }
        }

        public async Task OpenDepartmentCreate()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog =_dialogService.Show<DepartmentCreate>("Create department", options);
            var result = await dialog.Result;

            if(result.Data != null && (bool)result.Data)
            {
                await GetDepartments();
            }
        }

        public async Task Filter()
        {
            await GetDepartments();
        }

        public async Task BackToFullList()
        {
            DepartmentsOverview.MetaData.SearchString = "";
            await GetDepartments();
        }

        public async Task<TableData<DepartmentVM>> ServerReload(TableState state)
        {
            var searchString = DepartmentsOverview?.MetaData.SearchString ?? "";
            var sortString = state.GetSortString();

            var result = await _departmentService.GetAllAsync(sortString, state.Page, searchString, state.PageSize);

            return new TableData<DepartmentVM>() { TotalItems = result.MetaData.TotalRecords, Items = result.Departments };
        }
    }
}
