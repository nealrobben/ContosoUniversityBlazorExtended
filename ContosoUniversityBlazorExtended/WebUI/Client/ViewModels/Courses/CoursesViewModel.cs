using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Pages.Courses;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.Client.ViewModels.Courses
{
    public class CoursesViewModel : CoursesViewModelBase
    {
        private IDialogService _dialogService { get; set; }
        private ISnackbar _snackbar { get; set; }

        public CoursesOverviewVM coursesOverview { get; set; }

        public CoursesViewModel(CourseService courseService,
            IDialogService dialogService, ISnackbar snackbar) : base(courseService)
        {
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        public async Task Initialize()
        {
            coursesOverview = await _courseService.GetAllAsync();
        }

        public async Task DeleteCourse(int courseId, string title)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox("Confirm", $"Are you sure you want to delete the course '{title}'?",
                yesText: "Delete", cancelText: "Cancel");

            if (dialogResult == true)
            {
                var result = await _courseService.DeleteAsync(courseId.ToString());

                if (result.IsSuccessStatusCode)
                {
                    _snackbar.Add($"Deleted course {title}", Severity.Success);
                    coursesOverview = await _courseService.GetAllAsync();
                }
            }
        }

        public void OpenCourseDetails(int courseId)
        {
            var parameters = new DialogParameters();
            parameters.Add("CourseId", courseId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.ExtraSmall };

            _dialogService.Show<CourseDetails>("Course Details", parameters, options);
        }

        public async Task OpenCourseCreate()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

            var dialog = _dialogService.Show<CourseCreate>("Create course", options);
            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                coursesOverview = await _courseService.GetAllAsync();
            }
        }
    }
}
