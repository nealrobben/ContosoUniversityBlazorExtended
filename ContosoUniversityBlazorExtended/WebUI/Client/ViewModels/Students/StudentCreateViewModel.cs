using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentCreateViewModel : StudentViewModelBase
    {
        private MudDialogInstance _mudDialog;

        public CreateStudentCommand CreateStudentCommand { get; set; } = new CreateStudentCommand { EnrollmentDate = DateTime.Now };

        public bool ErrorVisible { get; set; }

        public StudentCreateViewModel(StudentService studentService)
            :base(studentService)
        {
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
    }
}
