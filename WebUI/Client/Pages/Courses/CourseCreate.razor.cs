using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Newtonsoft.Json;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Services;
using WebUI.Client.Shared;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace WebUI.Client.Pages.Courses;

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

    private CustomValidation _customValidation;

    protected override async Task OnInitializedAsync()
    {
        DepartmentsLookup = await DepartmentService.GetLookupAsync();
        CreateCourseCommand.DepartmentID = DepartmentsLookup.Departments.First().DepartmentID;
        StateHasChanged();
    }

    public async Task FormSubmitted(EditContext editContext)
    {
        _customValidation.ClearErrors();
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
            catch (ApiException ex)
            {
                var problemDetails = JsonConvert.DeserializeObject<ValidationProblemDetails>(ex.Response);

                if (problemDetails != null)
                {
                    _customValidation.DisplayErrors(problemDetails.Errors);
                }
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
