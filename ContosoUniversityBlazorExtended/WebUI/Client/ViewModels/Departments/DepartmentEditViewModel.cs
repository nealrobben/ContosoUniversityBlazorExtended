using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace WebUI.Client.ViewModels.Departments
{
    public class DepartmentEditViewModel : DepartmentViewModelBase
    {
        private readonly NavigationManager _navManager;
        private readonly InstructorService _instructorService;

        public UpdateDepartmentCommand UpdateDepartmentCommand { get; set; } = new UpdateDepartmentCommand();
        public InstructorsLookupVM InstructorsLookup { get; set; }

        private string _id;

        public DepartmentEditViewModel(DepartmentService departmentService,
            InstructorService instructorService, NavigationManager navManager)
            : base(departmentService)
        {
            _instructorService = instructorService;
            _navManager = navManager;
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;

            var department = await _departmentService.GetAsync(_id);

            UpdateDepartmentCommand.DepartmentID = department.DepartmentID;
            UpdateDepartmentCommand.Budget = department.Budget;
            UpdateDepartmentCommand.InstructorID = department.InstructorID.Value;
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
                    _navManager.NavigateTo("/departments");
                }
            }
        }
    }
}
