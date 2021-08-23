using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentsViewModel : StudentViewModelBase
    {
        private IDialogService _dialogService { get; set; }

        public StudentsOverviewVM StudentsOverview { get; set; } = new StudentsOverviewVM();

        public StudentsViewModel(StudentService studentService, IDialogService dialogService)
            : base(studentService)
        {
            _dialogService = dialogService;
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
    }
}
