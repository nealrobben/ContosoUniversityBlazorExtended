using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class Departments
    {
        [Inject]
        public DepartmentsViewModel DepartmentsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentsViewModel.OnInitializedAsync();
        }
    }
}
