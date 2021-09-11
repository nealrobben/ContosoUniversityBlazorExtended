using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentEdit
    {
        [Parameter]
        public string StudentId { get; set; }

        [Inject]
        public StudentEditViewModel StudentEditViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StudentEditViewModel.OnInitializedAsync(StudentId, MudDialog);
        }
    }
}
