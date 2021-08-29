using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorDetails
    {
        [Inject]
        public InstructorDetailsViewModel InstructorDetailsViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public int InstructorId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await InstructorDetailsViewModel.OnInitializedAsync(InstructorId.ToString());
        }

        public void Close()
        {
            MudDialog.Cancel();
        }
    }
}
