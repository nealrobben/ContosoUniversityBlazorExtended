using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorCreateViewModel : InstructorViewModelBase
    {
        private MudDialogInstance _mudDialog;

        public CreateInstructorCommand CreateInstructorCommand = new CreateInstructorCommand() { HireDate = DateTime.UtcNow.Date };

        public bool ErrorVisible { get; set; }

        public InstructorCreateViewModel(InstructorService instructorService)
            :base(instructorService)
        {
        }

        public void OnInitialized(MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            ErrorVisible = false;
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _instructorService.CreateAsync(CreateInstructorCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateInstructorCommand = new CreateInstructorCommand();
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
