using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.UpdateStudent;

namespace WebUI.Client.Pages.Students;

public partial class StudentEdit
{
    [Inject]
    public IFileuploadService _fileuploadService { get; set; }

    [Inject]
    public IStringLocalizer<StudentEdit> Localizer { get; set; }

    [Inject]
    public IStudentService StudentService { get; set; }

    [Parameter]
    public int StudentId { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    public UpdateStudentCommand UpdateStudentCommand { get; set; } = new UpdateStudentCommand();

    public bool ErrorVisible { get; set; }

    public IList<IBrowserFile> files { get; set; } = new List<IBrowserFile>();

    protected override async Task OnParametersSetAsync()
    {
        var student = await StudentService.GetAsync(StudentId.ToString());

        UpdateStudentCommand.StudentID = student.StudentID;
        UpdateStudentCommand.FirstName = student.FirstName;
        UpdateStudentCommand.LastName = student.LastName;
        UpdateStudentCommand.EnrollmentDate = student.EnrollmentDate;
        UpdateStudentCommand.ProfilePictureName = student.ProfilePictureName;
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
                    UpdateStudentCommand.ProfilePictureName = await _fileuploadService.UploadFile(files.First());
                }

                await StudentService.UpdateAsync(UpdateStudentCommand);
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
