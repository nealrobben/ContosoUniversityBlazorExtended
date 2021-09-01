using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentCreate
    {
        [Inject]
        DepartmentCreateViewModel DepartmentCreateViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentCreateViewModel.OnInitializedAsync(MudDialog);
        }
    }
}
