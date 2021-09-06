using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentCreate
    {
        [Inject]
        public StudentCreateViewModel StudentCreateViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StudentCreateViewModel.OnInitializedAsync(MudDialog);
        }
    }
}
