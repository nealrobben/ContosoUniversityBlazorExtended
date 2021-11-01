using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Pages.Students;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentsViewModel : StudentViewModelBase
    {
        private IDialogService _dialogService { get; set; }
        private ISnackbar _snackbar { get; set; }
        public MudTable<StudentOverviewVM> Table { get; set; }

        public StudentsOverviewVM StudentsOverview { get; set; } = new StudentsOverviewVM();

        public StudentsViewModel(StudentService studentService, 
            IDialogService dialogService, ISnackbar snackbar,
            IStringLocalizer<StudentResources> studentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(studentService,studentLocalizer,generalLocalizer)
        {
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        private async Task GetStudents()
        {
            await Table.ReloadServerData();
        }

        public async Task DeleteStudent(int studentId, string name)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox(_generalLocalizer["Confirm"], _studentLocalizer["DeleteConfirmation", name],
                yesText: _generalLocalizer["Delete"], cancelText: _generalLocalizer["Cancel"]);

            if (dialogResult == true)
            {
                var result = await _studentService.DeleteAsync(studentId.ToString());

                if (result.IsSuccessStatusCode)
                {
                    _snackbar.Add(_studentLocalizer["DeleteFeedback", name], Severity.Success);
                    await GetStudents();
                }
            }
        }

        public async Task Filter()
        {
            await GetStudents();
        }

        public async Task BackToFullList()
        {
            StudentsOverview.MetaData.SearchString = "";
            await GetStudents();
        }

        public void OpenStudentDetails(int studentId)
        {
            var parameters = new DialogParameters();
            parameters.Add("StudentId", studentId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            _dialogService.Show<StudentDetails>(_studentLocalizer["StudentDetails"], parameters, options);
        }

        public async Task OpenStudentEdit(int studentId)
        {
            var parameters = new DialogParameters();
            parameters.Add("StudentId", studentId.ToString());

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = _dialogService.Show<StudentEdit>(_studentLocalizer["StudentEdit"], parameters, options);

            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                await GetStudents();
            }
        }

        public async Task OpenStudentCreate()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = _dialogService.Show<StudentCreate>(_studentLocalizer["CreateStudent"], options);
            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                await GetStudents();
            }
        }

        public async Task<TableData<StudentOverviewVM>> ServerReload(TableState state)
        {
            var searchString = StudentsOverview?.MetaData.SearchString ?? "";
            var sortString = state.GetSortString();

            var result = await _studentService.GetAllAsync(sortString, state.Page, searchString, state.PageSize);

            return new TableData<StudentOverviewVM>() { TotalItems = result.MetaData.TotalRecords, Items = result.Students };
        }
    }
}