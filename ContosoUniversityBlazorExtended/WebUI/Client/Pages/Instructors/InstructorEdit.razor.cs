using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorEdit
    {
        [Parameter]
        public string InstructorId { get; set; }

        [Inject]
        public InstructorEditViewModel InstructorEditViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await InstructorEditViewModel.OnInitializedAsync(InstructorId, MudDialog);
        }
    }
}
