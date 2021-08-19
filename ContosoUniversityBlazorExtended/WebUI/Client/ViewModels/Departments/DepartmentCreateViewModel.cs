using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        private readonly NavigationManager _navManager;
        private readonly InstructorService _instructorService;

        public CreateDepartmentCommand CreateDepartmentCommand { get; set; } = new CreateDepartmentCommand() { StartDate = DateTime.UtcNow.Date };
        public InstructorsLookupVM InstructorsLookup { get; set; }

        public DepartmentCreateViewModel(DepartmentService departmentService, 
            InstructorService instructorService, NavigationManager navManager) 
            : base(departmentService)
        {
            _instructorService = instructorService;
            _navManager = navManager;
        }

        public async Task OnInitializedAsync()
        {
            InstructorsLookup = await _instructorService.GetLookupAsync();
            CreateDepartmentCommand.InstructorID = InstructorsLookup.Instructors.First().ID;
        }

        public async Task FormSubmitted(EditContext editContext)
        {
            bool formIsValid = editContext.Validate();

            if (formIsValid)
            {
                var result = await _departmentService.CreateAsync(CreateDepartmentCommand);

                if (result.IsSuccessStatusCode)
                {
                    CreateDepartmentCommand = new CreateDepartmentCommand();
                    _navManager.NavigateTo("/departments");
                }
            }
        }
    }
}
