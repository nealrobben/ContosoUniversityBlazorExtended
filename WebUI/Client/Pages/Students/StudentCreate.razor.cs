using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Client.Pages.Students;

public partial class StudentCreate
{
    [Inject]
    public IStringLocalizer<StudentCreate> Localizer { get; set; }

    [Inject]
    public IFileuploadService FileUploadService { get; set; }

    [Inject]
    public IStudentService StudentService { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    public CreateStudentCommand CreateStudentCommand { get; set; } = new CreateStudentCommand { EnrollmentDate = DateTime.Now.Date };

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
                    CreateStudentCommand.ProfilePictureName = await FileUploadService.UploadFile(files.First());
                }

                await StudentService.CreateAsync(CreateStudentCommand);

                CreateStudentCommand = new CreateStudentCommand();
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
