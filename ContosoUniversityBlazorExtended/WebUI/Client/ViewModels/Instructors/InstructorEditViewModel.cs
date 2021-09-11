using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorEditViewModel : InstructorViewModelBase
    {
        private MudDialogInstance _mudDialog;

        public UpdateInstructorCommand UpdateInstructorCommand = new UpdateInstructorCommand();

        private string _id;

        public bool ErrorVisible { get; set; }

        public InstructorEditViewModel(InstructorService instructorService)
            : base(instructorService)
        {
        }

        public async Task OnInitializedAsync(string id, MudDialogInstance MudDialog)
        {
            _id = id;
            _mudDialog = MudDialog;

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
