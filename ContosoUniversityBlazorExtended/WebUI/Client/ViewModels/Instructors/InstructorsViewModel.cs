using Microsoft.JSInterop;
using System.Threading.Tasks;
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
        private readonly IJSRuntime _jSRuntime;

        public InstructorsOverviewVM InstructorsOverview { get; set; }
        public CoursesForInstructorOverviewVM CourseForInstructorOverview { get; set; }
        public StudentsForCourseVM StudentsForCourse { get; set; }

        public int? SelectedInstructorId { get; set; }
        public int? SelectedCourseId { get; set; }

        public InstructorsViewModel(InstructorService instructorService, 
            CourseService courseService, StudentService studentService, IJSRuntime jSRuntime)
            : base(instructorService)
        {
            _courseService = courseService;
            _studentService = studentService;
            _jSRuntime = jSRuntime;
        }

        public async Task OnInitializedAsync()
        {
            InstructorsOverview = await _instructorService.GetAllAsync();
        }

        public async Task DeleteInstructor(int instructorId, string name)
        {
            if (!await _jSRuntime.InvokeAsync<bool>("confirm", $"Are you sure you want to delete the instructor '{name}'?"))
                return;

            var result = await _instructorService.DeleteAsync(instructorId.ToString());

            if (result.IsSuccessStatusCode)
            {
                InstructorsOverview = await _instructorService.GetAllAsync();
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
    }
}
