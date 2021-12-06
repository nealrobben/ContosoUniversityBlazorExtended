using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseEdit
    {
        [Inject]
        public ICourseService CourseService { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Parameter]
        public string CourseId { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        public UpdateCourseCommand UpdateCourseCommand { get; set; } = new UpdateCourseCommand();

        public DepartmentsLookupVM DepartmentsLookup { get; set; }

        public bool ErrorVisible { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var course = await CourseService.GetAsync(CourseId);

            UpdateCourseCommand.CourseID = course.CourseID;
            UpdateCourseCommand.Credits = course.Credits;
            UpdateCourseCommand.DepartmentID = course.DepartmentID;
            UpdateCourseCommand.Title = course.Title;

            DepartmentsLookup = await DepartmentService.GetLookupAsync();
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                try
                {
                    await CourseService.UpdateAsync(UpdateCourseCommand);
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
