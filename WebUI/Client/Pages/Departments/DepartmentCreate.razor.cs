using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Services;
using WebUI.Client.Shared;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace WebUI.Client.Pages.Departments;

public partial class DepartmentCreate
{
    [Inject]
    IDepartmentService DepartmentService { get; set; }

    [Inject]
    IInstructorService InstructorService { get; set; }
    
    [Inject]
    IStringLocalizer<DepartmentCreate> Localizer { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    private CustomValidation _customValidation;

    public CreateDepartmentCommand CreateDepartmentCommand { get; set; } = new CreateDepartmentCommand() { StartDate = DateTime.UtcNow.Date };
    public InstructorsLookupVM InstructorsLookup { get; set; }

    public bool ErrorVisible { get; set; }

    protected override async Task OnInitializedAsync()
    {
        InstructorsLookup = await InstructorService.GetLookupAsync();
        CreateDepartmentCommand.InstructorID = InstructorsLookup.Instructors.First().ID;
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
                await DepartmentService.CreateAsync(CreateDepartmentCommand);

                CreateDepartmentCommand = new CreateDepartmentCommand();
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
            catch (Exception)
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
