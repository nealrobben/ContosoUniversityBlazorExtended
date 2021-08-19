using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentCreateViewModel : StudentViewModelBase
    {
        private readonly NavigationManager _navManager;

        public CreateStudentCommand CreateStudentCommand { get; set; } = new CreateStudentCommand { EnrollmentDate = DateTime.Now };

        public StudentCreateViewModel(StudentService studentService, NavigationManager navManager)
            :base(studentService)
        {
            _navManager = navManager;
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
                    _navManager.NavigateTo("/students");
                }
            }
        }
    }
}
