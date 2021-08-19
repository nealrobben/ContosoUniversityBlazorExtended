using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorCreateViewModel : InstructorViewModelBase
    {
        private readonly NavigationManager _navManager;

        public CreateInstructorCommand CreateInstructorCommand = new CreateInstructorCommand() { HireDate = DateTime.UtcNow.Date };

        public InstructorCreateViewModel(InstructorService instructorService,
             NavigationManager navManager)
            :base(instructorService)
        {
            _navManager = navManager;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _instructorService.CreateAsync(CreateInstructorCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateInstructorCommand = new CreateInstructorCommand();
                    _navManager.NavigateTo("/instructors");
                }
            }
        }
    }
}
