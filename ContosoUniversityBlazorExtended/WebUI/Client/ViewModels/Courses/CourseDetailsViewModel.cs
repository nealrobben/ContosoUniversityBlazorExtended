using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Queries.GetCourseDetails;

namespace WebUI.Client.ViewModels.Courses
{
    public class CourseDetailsViewModel : CoursesViewModelBase
    {
        private string _id;

        public CourseDetailVM Course { get; set; }

        public CourseDetailsViewModel(ICourseService courseService,
            IStringLocalizer<CourseResources> courseLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(courseService,courseLocalizer,generalLocalizer)
        {
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;
            Course = await _courseService.GetAsync(id);
        }
    }
}
