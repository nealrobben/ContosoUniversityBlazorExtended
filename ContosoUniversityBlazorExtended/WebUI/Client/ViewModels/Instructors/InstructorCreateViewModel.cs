using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorCreateViewModel : InstructorViewModelBase
    {
        private MudDialogInstance _mudDialog;
        private FileuploadService _fileuploadService;

        public CreateInstructorCommand CreateInstructorCommand = new CreateInstructorCommand() { HireDate = DateTime.UtcNow.Date };

        public bool ErrorVisible { get; set; }

        public IList<IBrowserFile> files { get; set; }

        public InstructorCreateViewModel(IInstructorService instructorService, 
            FileuploadService fileuploadService, IStringLocalizer<InstructorResources> instructorLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            :base(instructorService,instructorLocalizer,generalLocalizer)
        {
            files = new List<IBrowserFile>();
            _fileuploadService = fileuploadService;
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
                if (files.Any())
                {
                    CreateInstructorCommand.ProfilePictureName = await _fileuploadService.UploadFile(files.First());
                }

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

        public void UploadFiles(InputFileChangeEventArgs e)
        {
            foreach (var file in e.GetMultipleFiles())
            {
                files.Add(file);
            }
        }
    }
}
