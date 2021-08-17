using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace WebUI.Client.ViewModels.Courses
{
    public class CourseCreateViewModel : CoursesViewModelBase
    {
        private readonly NavigationManager _navManager;
        private readonly DepartmentService _departmentService;

        public CreateCourseCommand CreateCourseCommand = new CreateCourseCommand();
        public DepartmentsLookupVM DepartmentsLookup { get; set; }

        public CourseCreateViewModel(CourseService courseService, DepartmentService departmentService)
            :base(courseService)
        {
            _departmentService = departmentService;
        }

        public async Task OnInitializedAsync()
        {
            DepartmentsLookup = await _departmentService.GetLookupAsync();
            CreateCourseCommand.DepartmentID = DepartmentsLookup.Departments.First().DepartmentID;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _courseService.CreateAsync(CreateCourseCommand);

                if (result.IsSuccessStatusCode)
                {
                    _navManager.NavigateTo("/courses");
                }
            }
        }
    }
}
