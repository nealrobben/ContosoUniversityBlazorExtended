using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.UpdateStudent;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentEditViewModel : StudentViewModelBase
    {
        private readonly NavigationManager _navManager;

        public UpdateStudentCommand UpdateStudentCommand { get; set; } = new UpdateStudentCommand();

        public StudentEditViewModel(StudentService studentService, NavigationManager navManager)
            : base(studentService)
        {
            _navManager = navManager;
        }

        public async Task OnInitializedAsync(string id)
        {
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
                    _navManager.NavigateTo("/students");
                }
            }
        }
    }
}
