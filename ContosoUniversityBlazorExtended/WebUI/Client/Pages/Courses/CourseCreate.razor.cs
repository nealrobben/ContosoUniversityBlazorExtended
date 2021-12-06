using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseCreate
    {
        [Inject]
        public IStringLocalizer<CourseCreate> Localizer { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public ICourseService CourseService { get; set; }

        public CreateCourseCommand CreateCourseCommand = new CreateCourseCommand();
        public DepartmentsLookupVM DepartmentsLookup { get; set; }

        public bool ErrorVisible { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            DepartmentsLookup = await DepartmentService.GetLookupAsync();
            CreateCourseCommand.DepartmentID = DepartmentsLookup.Departments.First().DepartmentID;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            ErrorVisible = false;
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                try
                {
                    await CourseService.CreateAsync(CreateCourseCommand);

                    CreateCourseCommand = new CreateCourseCommand();
                    MudDialog.Close(DialogResult.Ok(true));
                }
                catch (System.Exception)
                {
                    ErrorVisible = true;
                }
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
        }
    }
}
