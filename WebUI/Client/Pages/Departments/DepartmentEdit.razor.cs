using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using MudBlazor;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Services;
using WebUI.Client.Shared;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace WebUI.Client.Pages.Departments;

public partial class DepartmentEdit
{
    [Parameter]
    public int DepartmentId { get; set; }

    [Inject]
    public IDepartmentService DepartmentService { get; set; }

    [Inject]
    IStringLocalizer<DepartmentEdit> Localizer { get; set; }

    [Inject]
    public IInstructorService InstructorService { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    private CustomValidation _customValidation;

    public bool ErrorVisible { get; set; }

    public UpdateDepartmentCommand UpdateDepartmentCommand { get; set; } = new UpdateDepartmentCommand();
    public InstructorsLookupVM InstructorsLookup { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        var department = await DepartmentService.GetAsync(DepartmentId.ToString());

        UpdateDepartmentCommand.DepartmentID = department.DepartmentID;
        UpdateDepartmentCommand.Budget = department.Budget;
        UpdateDepartmentCommand.InstructorID = department.InstructorID ?? 0;
        UpdateDepartmentCommand.Name = department.Name;
        UpdateDepartmentCommand.StartDate = department.StartDate;
        UpdateDepartmentCommand.RowVersion = department.RowVersion;

        InstructorsLookup = await InstructorService.GetLookupAsync();
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
                await DepartmentService.UpdateAsync(UpdateDepartmentCommand);
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
