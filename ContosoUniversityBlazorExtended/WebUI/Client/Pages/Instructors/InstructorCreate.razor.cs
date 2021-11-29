﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorCreate
    {
        [Inject]
        public IStringLocalizer<InstructorCreate> Localizer { get; set; }

        [Inject]
        public FileuploadService _fileuploadService { get; set; }

        [Inject]
        public IInstructorService InstructorService { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        public CreateInstructorCommand CreateInstructorCommand = new CreateInstructorCommand() { HireDate = DateTime.UtcNow.Date };

        public IList<IBrowserFile> files { get; set; } = new List<IBrowserFile>();

        public bool ErrorVisible { get; set; }

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

                var result = await InstructorService.CreateAsync(CreateInstructorCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateInstructorCommand = new CreateInstructorCommand();
                    MudDialog.Close(DialogResult.Ok(true));
                }
                else
                {
                    ErrorVisible = true;
                }
            }
        }

        public void Cancel()
        {
            MudDialog.Cancel();
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
