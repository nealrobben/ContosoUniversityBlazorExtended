using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System;
using System.Linq;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentCreateViewModel : DepartmentViewModelBase
    {
        private readonly InstructorService _instructorService;
        private MudDialogInstance _mudDialog;

        public CreateDepartmentCommand CreateDepartmentCommand { get; set; } = new CreateDepartmentCommand() { StartDate = DateTime.UtcNow.Date };
        public InstructorsLookupVM InstructorsLookup { get; set; }

        public bool ErrorVisible { get; set; }

        public DepartmentCreateViewModel(IDepartmentService departmentService, 
            InstructorService instructorService, IStringLocalizer<DepartmentResources> departmentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer) 
            : base(departmentService, departmentLocalizer, generalLocalizer)
        {
            _instructorService = instructorService;
        }

        public async Task OnInitializedAsync(MudDialogInstance MudDialog)
        {
            _mudDialog = MudDialog;
            InstructorsLookup = await _instructorService.GetLookupAsync();
            CreateDepartmentCommand.InstructorID = InstructorsLookup.Instructors.First().ID;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            ErrorVisible = false;
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _departmentService.CreateAsync(CreateDepartmentCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateDepartmentCommand = new CreateDepartmentCommand();
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
