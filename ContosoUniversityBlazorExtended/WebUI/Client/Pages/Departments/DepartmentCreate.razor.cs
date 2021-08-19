using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentCreate
    {
        [Inject]
        DepartmentCreateViewModel DepartmentCreateViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentCreateViewModel.OnInitializedAsync();
        }
    }
}
