using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.Client.ViewModels.Courses
{
    public class CoursesViewModel : CoursesViewModelBase
    {
        private IDialogService _dialogService { get; set; }

        public CoursesOverviewVM coursesOverview { get; set; }

        public CoursesViewModel(CourseService courseService,
            IDialogService dialogService) : base(courseService)
        {
            _dialogService = dialogService;
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
                    coursesOverview = await _courseService.GetAllAsync();
                }
            }
        }
    }
}
