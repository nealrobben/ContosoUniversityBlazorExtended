using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentCreate
    {
        [Inject]
        public FileuploadService _fileuploadService { get; set; }

        [Inject]
        public IStudentService StudentService { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        public CreateStudentCommand CreateStudentCommand { get; set; } = new CreateStudentCommand { EnrollmentDate = DateTime.Now };

        public bool ErrorVisible { get; set; }

        public IList<IBrowserFile> files { get; set; } = new List<IBrowserFile>();

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                if (files.Any())
                {
                    CreateStudentCommand.ProfilePictureName = await _fileuploadService.UploadFile(files.First());
                }

                var result = await StudentService.CreateAsync(CreateStudentCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateStudentCommand = new CreateStudentCommand();
                    MudDialog.Close(DialogResult.Ok(true));
                }
                else
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
}
