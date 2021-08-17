using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorEditViewModel : InstructorViewModelBase
    {
        private readonly NavigationManager _navManager;

        public UpdateInstructorCommand UpdateInstructorCommand = new UpdateInstructorCommand();

        private string _id;

        public InstructorEditViewModel(InstructorService instructorService, NavigationManager navManager)
            : base(instructorService)
        {
            _navManager = navManager;
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;

            var instructor = await _instructorService.GetAsync(id);

            UpdateInstructorCommand.InstructorID = instructor.InstructorID;
            UpdateInstructorCommand.FirstName = instructor.FirstName;
            UpdateInstructorCommand.LastName = instructor.LastName;
            UpdateInstructorCommand.HireDate = instructor.HireDate;
            UpdateInstructorCommand.OfficeLocation = instructor.OfficeLocation;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _instructorService.UpdateAsync(UpdateInstructorCommand);

                if (result.IsSuccessStatusCode)
                {
                   _navManager.NavigateTo("/instructors");
                }
            }
        }
    }
}
