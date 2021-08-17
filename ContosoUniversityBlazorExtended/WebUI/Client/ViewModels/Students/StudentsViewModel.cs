using Microsoft.JSInterop;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentsViewModel : StudentViewModelBase
    {
        private readonly IJSRuntime _jSRuntime;

        public StudentsOverviewVM StudentsOverview { get; set; } = new StudentsOverviewVM();

        public StudentsViewModel(StudentService studentService, IJSRuntime jSRuntime)
            : base(studentService)
        {
            _jSRuntime = jSRuntime;
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
            if (!await _jSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete the student '{name}'?"))
                return;

            var result = await _studentService.DeleteAsync(studentId.ToString());

            if (result.IsSuccessStatusCode)
            {
                await GetStudents();
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
