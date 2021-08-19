using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Courses
{
    public class CoursesViewModelBase
    {
        protected readonly CourseService _courseService;

        public CoursesViewModelBase(CourseService courseService)
        {
            _courseService = courseService;
        }
    }
}
