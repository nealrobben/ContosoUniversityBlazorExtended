using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentDetails
    {
        [Inject]
        public DepartmentDetailsViewModel DepartmentDetailsViewModel { get; set; }

        [CascadingParameter] 
        MudDialogInstance MudDialog { get; set; }

        [Parameter] 
        public int DepartmentId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentDetailsViewModel.OnInitializedAsync(MudDialog, DepartmentId.ToString());
        }
    }
}
