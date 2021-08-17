using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentEdit
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public DepartmentEditViewModel DepartmentEditViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentEditViewModel.OnInitializedAsync(id);
        }
    }
}
