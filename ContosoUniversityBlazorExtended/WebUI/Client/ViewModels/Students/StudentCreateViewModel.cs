using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentCreateViewModel : StudentViewModelBase
    {
        private MudDialogInstance _mudDialog;
        private FileuploadService _fileuploadService;

        public CreateStudentCommand CreateStudentCommand { get; set; } = new CreateStudentCommand { EnrollmentDate = DateTime.Now };

        public bool ErrorVisible { get; set; }

        public IList<IBrowserFile> files { get; set; }

        public StudentCreateViewModel(StudentService studentService, FileuploadService fileuploadService, 
            IStringLocalizer<StudentResources> studentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            :base(studentService,studentLocalizer,generalLocalizer)
        {
            files = new List<IBrowserFile>();
            _fileuploadService = fileuploadService;
        }

        public async Task OnInitializedAsync(MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                if (files.Any())
                {
                    CreateStudentCommand.ProfilePictureName = await _fileuploadService.UploadFile(files.First());
                }

                var result = await _studentService.CreateAsync(CreateStudentCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateStudentCommand = new CreateStudentCommand();
                    _mudDialog.Close(DialogResult.Ok(true));
                }
                else
                {
                    ErrorVisible = true;
                }
            }
        }

        public void Cancel()
        {
            _mudDialog.Cancel();
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
