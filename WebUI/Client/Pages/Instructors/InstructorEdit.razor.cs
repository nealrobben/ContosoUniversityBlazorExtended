using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;

namespace WebUI.Client.Pages.Instructors;

public partial class InstructorEdit
{
    [Inject]
    public IFileuploadService _fileuploadService { get; set; }

    [Inject]
    public IStringLocalizer<InstructorEdit> Localizer { get; set; }

    [Inject]
    public IInstructorService InstructorService { get; set; }

    [Parameter]
    public int InstructorId { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    public UpdateInstructorCommand UpdateInstructorCommand = new UpdateInstructorCommand();

    public bool ErrorVisible { get; set; }

    public IList<IBrowserFile> files { get; set; } = new List<IBrowserFile>();

    protected override async Task OnParametersSetAsync()
    {
        var instructor = await InstructorService.GetAsync(InstructorId.ToString());

        UpdateInstructorCommand.InstructorID = instructor.InstructorID;
        UpdateInstructorCommand.FirstName = instructor.FirstName;
        UpdateInstructorCommand.LastName = instructor.LastName;
        UpdateInstructorCommand.HireDate = instructor.HireDate;
        UpdateInstructorCommand.OfficeLocation = instructor.OfficeLocation;
        UpdateInstructorCommand.ProfilePictureName = instructor.ProfilePictureName;
    }

    public async Task FormSubmitted(EditContext editContext)
    {
        bool formIsValid = editContext.Validate();

        if (formIsValid)
        {
            try
            {
                if (files.Any())
                {
                    UpdateInstructorCommand.ProfilePictureName = await _fileuploadService.UploadFile(files.First());
                }

                await InstructorService.UpdateAsync(UpdateInstructorCommand);
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

    public void UploadFiles(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            files.Add(file);
        }
    }
}
