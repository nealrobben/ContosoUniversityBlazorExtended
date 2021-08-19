using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace WebUI.Client.ViewModels.Courses
{
    public class CourseEditViewModel : CoursesViewModelBase
    {
        private readonly NavigationManager _navManager;
        private readonly DepartmentService _departmentService;

        public UpdateCourseCommand UpdateCourseCommand { get; set; } = new UpdateCourseCommand();
        public DepartmentsLookupVM DepartmentsLookup { get; set; }

        private string _id;

        public CourseEditViewModel(CourseService courseService, 
            DepartmentService departmentService, NavigationManager navManager)
            : base(courseService)
        {
            _departmentService = departmentService;
            _navManager = navManager;
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;

            var course = await _courseService.GetAsync(id);

            UpdateCourseCommand.CourseID = course.CourseID;
            UpdateCourseCommand.Credits = course.Credits;
            UpdateCourseCommand.DepartmentID = course.DepartmentID;
            UpdateCourseCommand.Title = course.Title;

            DepartmentsLookup = await _departmentService.GetLookupAsync();
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _courseService.UpdateAsync(UpdateCourseCommand);

                if (result.IsSuccessStatusCode)
                {
                    _navManager.NavigateTo("/courses");
                }
            }
        }
    }
}
