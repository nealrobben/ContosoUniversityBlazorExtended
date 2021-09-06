using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Pages.Students;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentsViewModel : StudentViewModelBase
    {
        private IDialogService _dialogService { get; set; }
        private ISnackbar _snackbar { get; set; }

        public StudentsOverviewVM StudentsOverview { get; set; } = new StudentsOverviewVM();

        public StudentsViewModel(StudentService studentService, 
            IDialogService dialogService, ISnackbar snackbar)
            : base(studentService)
        {
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        public async Task OnInitializedAsync()
        {
            await GetStudents();
        }

        private async Task GetStudents()
        {
            var pageNumber = StudentsOverview?.PageNumber;
            var searchString = StudentsOverview?.CurrentFilter ?? "";
            var sortOrder = StudentsOverview.CurrentSort ?? "";

            StudentsOverview = await _studentService.GetAllAsync(sortOrder, pageNumber, searchString);
        }

        public async Task DeleteStudent(int studentId, string name)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox("Confirm", $"Are you sure you want to delete the student '{name}'?",
                yesText: "Delete", cancelText: "Cancel");

            if (dialogResult == true)
            {
                var result = await _studentService.DeleteAsync(studentId.ToString());

                if (result.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Deleted student {name}", Severity.Success);
                    await GetStudents();
                }
            }
        }

        public async Task PreviousPage()
        {
            if (StudentsOverview.PageNumber > 1)
                StudentsOverview.PageNumber -= 1;

            await GetStudents();
        }

        public async Task NextPage()
        {
            if (StudentsOverview.PageNumber < StudentsOverview.TotalPages)
                StudentsOverview.PageNumber += 1;

            await GetStudents();
        }

        public async Task Filter()
        {
            await GetStudents();
        }

        public async Task BackToFullList()
        {
            StudentsOverview.CurrentFilter = "";
            await GetStudents();
        }

        public async Task SortByLastName()
        {
            if (StudentsOverview.CurrentSort == "" || StudentsOverview.CurrentSort == null)
            {
                StudentsOverview.CurrentSort = "name_desc";
            }
            else
            {
                StudentsOverview.CurrentSort = "";
            }

            await GetStudents();
        }

        public async Task SortByEnrollmentDate()
        {
            if (StudentsOverview.CurrentSort == "Date")
            {
                StudentsOverview.CurrentSort = "date_desc";
            }
            else
            {
                StudentsOverview.CurrentSort = "Date";
            }

            await GetStudents();
        }

        public void OpenStudentDetails(int studentId)
        {
            var parameters = new DialogParameters();
            parameters.Add("StudentId", studentId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Small };

            _dialogService.Show<StudentDetails>("Student Details", parameters, options);
        }

        public async Task OpenStudentCreate()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = _dialogService.Show<StudentCreate>("Create student", options);
            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                await GetStudents();
            }
        }
    }
}
