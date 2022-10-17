using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Client.Pages.Instructors;

public partial class InstructorCreate
{
    [Inject]
    public IStringLocalizer<InstructorCreate> Localizer { get; set; }

    [Inject]
    public IFileuploadService FileUploadService { get; set; }

    [Inject]
    public IInstructorService InstructorService { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    public CreateInstructorCommand CreateInstructorCommand = new CreateInstructorCommand() { HireDate = DateTime.Now.Date };

    public bool ErrorVisible { get; set; }

    public IList<IBrowserFile> files { get; set; } = new List<IBrowserFile>();

    public async Task FormSubmitted(EditContext editContext)
    {
        ErrorVisible = false;
        bool formIsValid = editContext.Validate();

        if (formIsValid)
        {
            try
            {
                if (files.Any())
                {
                    CreateInstructorCommand.ProfilePictureName = await FileUploadService.UploadFile(files.First());
                }

                await InstructorService.CreateAsync(CreateInstructorCommand);

                CreateInstructorCommand = new CreateInstructorCommand();
                MudDialog.Close(DialogResult.Ok(true));
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

    public void UploadFiles(InputFileChangeEventArgs e)
    {
        foreach (var file in e.GetMultipleFiles())
        {
            files.Add(file);
        }
    }
}
