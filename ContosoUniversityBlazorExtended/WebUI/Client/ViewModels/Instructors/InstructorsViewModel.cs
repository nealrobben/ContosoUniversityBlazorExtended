using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Pages.Instructors;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorsViewModel : InstructorViewModelBase
    {
        private readonly CourseService _courseService;
        private readonly StudentService _studentService;
        private ISnackbar _snackbar { get; set; }
        private IDialogService _dialogService { get; set; }

        public InstructorsOverviewVM InstructorsOverview { get; set; } = new InstructorsOverviewVM();
        public CoursesForInstructorOverviewVM CourseForInstructorOverview { get; set; }
        public StudentsForCourseVM StudentsForCourse { get; set; }

        public int? SelectedInstructorId { get; set; }
        public int? SelectedCourseId { get; set; }

        public InstructorsViewModel(InstructorService instructorService, 
            CourseService courseService, StudentService studentService, 
            IDialogService dialogService, ISnackbar snackbar)
            : base(instructorService)
        {
            _courseService = courseService;
            _studentService = studentService;
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        public async Task OnInitializedAsync()
        {
            InstructorsOverview = await _instructorService.GetAllAsync("",null,"",null);
        }

        public async Task DeleteInstructor(int instructorId, string name)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox("Confirm", $"Are you sure you want to delete the instructor '{name}'?",
                yesText: "Delete", cancelText: "Cancel");
            
            if (dialogResult == true)
            {
                var result = await _instructorService.DeleteAsync(instructorId.ToString());

                if (result.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Deleted instructor {name}", Severity.Success);
                    InstructorsOverview = await _instructorService.GetAllAsync("", null, "", null);
                }
            }
        }

        public async Task SelectInstructor(int instructorId)
        {
            SelectedInstructorId = instructorId;
            SelectedCourseId = null;
            StudentsForCourse = null;

            CourseForInstructorOverview = await _courseService.GetCoursesForInstructor(instructorId.ToString());
        }

        public async Task SelectCourse(int courseId)
        {
            SelectedCourseId = courseId;

            StudentsForCourse = await _studentService.GetStudentsForCourse(courseId.ToString());
        }

        public void OpenInstructorDetails(int instructorId)
        {
            var parameters = new DialogParameters();
            parameters.Add("InstructorId", instructorId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

            _dialogService.Show<InstructorDetails>("Instructor Details", parameters, options);
        }

        public async Task OpenInstructorEdit(int instructorId)
        {
            var parameters = new DialogParameters();
            parameters.Add("InstructorId", instructorId.ToString());

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

            var dialog = _dialogService.Show<InstructorEdit>("Instructor Edit", parameters, options);

            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                InstructorsOverview = await _instructorService.GetAllAsync("", null, "", null);
            }
        }

        public async Task OpenCreateInstructor()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

            var dialog = _dialogService.Show<InstructorCreate>("Create instructor", options);
            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                InstructorsOverview = await _instructorService.GetAllAsync("", null, "", null);
            }
        }
    }
}
