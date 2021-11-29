using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentEdit
    {
        [Parameter]
        public int DepartmentId { get; set; }

        [Inject]
        public IDepartmentService DepartmentService { get; set; }

        [Inject]
        public IInstructorService InstructorService { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        public bool ErrorVisible { get; set; }

        public UpdateDepartmentCommand UpdateDepartmentCommand { get; set; } = new UpdateDepartmentCommand();
        public InstructorsLookupVM InstructorsLookup { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var department = await DepartmentService.GetAsync(DepartmentId.ToString());

            UpdateDepartmentCommand.DepartmentID = department.DepartmentID;
            UpdateDepartmentCommand.Budget = department.Budget;
            UpdateDepartmentCommand.InstructorID = department.InstructorID ?? 0;
            UpdateDepartmentCommand.Name = department.Name;
            UpdateDepartmentCommand.StartDate = department.StartDate;
            UpdateDepartmentCommand.RowVersion = department.RowVersion;

            InstructorsLookup = await InstructorService.GetLookupAsync();
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await DepartmentService.UpdateAsync(UpdateDepartmentCommand);

                if (result.IsSuccessStatusCode)
                {
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
    }
}
