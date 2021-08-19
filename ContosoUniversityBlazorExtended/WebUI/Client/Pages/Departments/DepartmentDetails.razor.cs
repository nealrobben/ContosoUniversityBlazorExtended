using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentDetails
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public DepartmentDetailsViewModel DepartmentDetailsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentDetailsViewModel.OnInitializedAsync(id);
        }
    }
}
