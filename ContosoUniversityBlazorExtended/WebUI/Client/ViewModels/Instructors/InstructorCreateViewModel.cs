using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorCreateViewModel : InstructorViewModelBase
    {
        private MudDialogInstance _mudDialog;
        private HttpClient _http;

        public CreateInstructorCommand CreateInstructorCommand = new CreateInstructorCommand() { HireDate = DateTime.UtcNow.Date };

        public bool ErrorVisible { get; set; }

        public IList<IBrowserFile> files { get; set; }

        public InstructorCreateViewModel(InstructorService instructorService, HttpClient http)
            :base(instructorService)
        {
            files = new List<IBrowserFile>();
            _http = http;
        }

        public void OnInitialized(MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            ErrorVisible = false;
            bool formIsValid = editContext.Validate();
            var storedFileName = "";

            if (formIsValid)
            {
                long maxFileSize = 1024 * 1024 * 15;
                using var content = new MultipartFormDataContent();

                foreach(var file in files)
                {
                    try
                    {
                        var fileContent = new StreamContent(file.OpenReadStream(maxFileSize));

                        content.Add(content: fileContent,name: "\"files\"",fileName: file.Name);

                        var response = await _http.PostAsync("/api/Filesave", content);

                        var newUploadResults = await response.Content.ReadFromJsonAsync<IList<UploadResult>>();

                        if(newUploadResults.Count != 0)
                        {
                            storedFileName = newUploadResults.First().StoredFileName;
                        }
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }

                CreateInstructorCommand.ProfilePictureName = storedFileName;
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
