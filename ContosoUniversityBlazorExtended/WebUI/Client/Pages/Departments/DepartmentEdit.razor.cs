using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Departments;

namespace WebUI.Client.Pages.Departments
{
    public partial class DepartmentEdit
    {
        [Parameter]
        public int DepartmentId { get; set; }

        [Inject]
        public DepartmentEditViewModel DepartmentEditViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await DepartmentEditViewModel.OnInitializedAsync(DepartmentId.ToString(), MudDialog);
        }
    }
}
