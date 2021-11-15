using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
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
        private readonly IDepartmentService _departmentService;
        private MudDialogInstance _mudDialog;

        public CreateCourseCommand CreateCourseCommand = new CreateCourseCommand();
        public DepartmentsLookupVM DepartmentsLookup { get; set; }

        public bool ErrorVisible { get; set; }

        public CourseCreateViewModel(ICourseService courseService, 
            IDepartmentService departmentService, NavigationManager navManager, 
            IStringLocalizer<CourseResources> courseLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            :base(courseService,courseLocalizer,generalLocalizer)
        {
            _departmentService = departmentService;
            _navManager = navManager;
        }

        public async Task OnInitializedAsync(MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
            DepartmentsLookup = await _departmentService.GetLookupAsync();
            CreateCourseCommand.DepartmentID = DepartmentsLookup.Departments.First().DepartmentID;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            ErrorVisible = false;
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _courseService.CreateAsync(CreateCourseCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateCourseCommand = new CreateCourseCommand();
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
