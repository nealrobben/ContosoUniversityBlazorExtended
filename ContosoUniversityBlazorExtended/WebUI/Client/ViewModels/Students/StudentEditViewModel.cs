using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.UpdateStudent;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentEditViewModel : StudentViewModelBase
    {
        private MudDialogInstance _mudDialog;
        private FileuploadService _fileuploadService;

        public UpdateStudentCommand UpdateStudentCommand { get; set; } = new UpdateStudentCommand();

        public bool ErrorVisible { get; set; }

        public IList<IBrowserFile> files { get; set; }

        public StudentEditViewModel(StudentService studentService, FileuploadService fileuploadService)
            : base(studentService)
        {
            files = new List<IBrowserFile>();
            _fileuploadService = fileuploadService;
        }

        public async Task OnInitializedAsync(string id, MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
            var student = await _studentService.GetAsync(id);

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
                if (files.Any())
                {
                    UpdateStudentCommand.ProfilePictureName = await _fileuploadService.UploadFile(files.First());
                }

                var result = await _studentService.UpdateAsync(UpdateStudentCommand);

                if (result.IsSuccessStatusCode)
                {
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
