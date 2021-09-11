using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.UpdateStudent;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentEditViewModel : StudentViewModelBase
    {
        private MudDialogInstance _mudDialog;

        public UpdateStudentCommand UpdateStudentCommand { get; set; } = new UpdateStudentCommand();

        public bool ErrorVisible { get; set; }

        public StudentEditViewModel(StudentService studentService)
            : base(studentService)
        {
        }

        public async Task OnInitializedAsync(string id, MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
            var student = await _studentService.GetAsync(id);

            UpdateStudentCommand.StudentID = student.StudentID;
            UpdateStudentCommand.FirstName = student.FirstName;
            UpdateStudentCommand.LastName = student.LastName;
            UpdateStudentCommand.EnrollmentDate = student.EnrollmentDate;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
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
    }
}
