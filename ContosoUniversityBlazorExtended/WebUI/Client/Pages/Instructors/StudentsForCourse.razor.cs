using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace WebUI.Client.Pages.Instructors
{
    public partial class StudentsForCourse
    {
        [Inject]
        public IStringLocalizer<Instructors> Localizer { get; set; }

        [Inject]
        public IStudentService StudentService { get; set; }

        public StudentsForCourseVM StudentsForCourseVM { get; set; }
    }
}
