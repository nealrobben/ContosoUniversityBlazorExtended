using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentEditViewModel : DepartmentViewModelBase
    {
        private readonly InstructorService _instructorService;
        private MudDialogInstance _mudDialog;

        public UpdateDepartmentCommand UpdateDepartmentCommand { get; set; } = new UpdateDepartmentCommand();
        public InstructorsLookupVM InstructorsLookup { get; set; }

        private string _id;

        public bool ErrorVisible { get; set; }

        public DepartmentEditViewModel(DepartmentService departmentService,
            InstructorService instructorService, IStringLocalizer<DepartmentResources> departmentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(departmentService, departmentLocalizer, generalLocalizer)
        {
            _instructorService = instructorService;
        }

        public async Task OnInitializedAsync(string id, MudDialogInstance MudDialog)
        {
            _id = id;
            _mudDialog = MudDialog;

            var department = await _departmentService.GetAsync(_id);

            UpdateDepartmentCommand.DepartmentID = department.DepartmentID;
            UpdateDepartmentCommand.Budget = department.Budget;
            UpdateDepartmentCommand.InstructorID = department.InstructorID ?? 0;
            UpdateDepartmentCommand.Name = department.Name;
            UpdateDepartmentCommand.StartDate = department.StartDate;
            UpdateDepartmentCommand.RowVersion = department.RowVersion;

            InstructorsLookup = await _instructorService.GetLookupAsync();
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _departmentService.UpdateAsync(UpdateDepartmentCommand);

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
