using Microsoft.Extensions.Localization;
using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Courses
{
    public class CoursesViewModelBase
    {
        protected readonly ICourseService _courseService;

        protected readonly IStringLocalizer<CourseResources> _courseLocalizer;
        protected readonly IStringLocalizer<GeneralResources> _generalLocalizer;

        public CoursesViewModelBase(ICourseService courseService, IStringLocalizer<CourseResources> courseLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
        {
            _courseService = courseService;
            _courseLocalizer = courseLocalizer;
            _generalLocalizer = generalLocalizer;
        }
    }
}
