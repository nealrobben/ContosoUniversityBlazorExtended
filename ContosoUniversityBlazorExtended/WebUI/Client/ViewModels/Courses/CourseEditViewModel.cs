using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace WebUI.Client.ViewModels.Courses
{
    public class CourseEditViewModel : CoursesViewModelBase
    {
        private readonly IDepartmentService _departmentService;
        private MudDialogInstance _mudDialog;

        public UpdateCourseCommand UpdateCourseCommand { get; set; } = new UpdateCourseCommand();
        public DepartmentsLookupVM DepartmentsLookup { get; set; }

        private string _id;

        public bool ErrorVisible { get; set; }

        public CourseEditViewModel(CourseService courseService, 
            IDepartmentService departmentService,
            IStringLocalizer<CourseResources> courseLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(courseService, courseLocalizer, generalLocalizer)
        {
            _departmentService = departmentService;
        }

        public async Task OnInitializedAsync(string id, MudDialogInstance MudDialog)
        {
            _id = id;
            _mudDialog = MudDialog;

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
                    _mudDialog.Close(DialogResult.Ok(true));
                }
                else
                {
                    ErrorVisible = true;
                }
            }
        }

        public void Cancel()
        {
            _mudDialog.Cancel();
        }
    }
}
