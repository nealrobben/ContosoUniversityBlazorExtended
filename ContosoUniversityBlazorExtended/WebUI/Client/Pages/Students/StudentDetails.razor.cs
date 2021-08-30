using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentDetails
    {
        [Inject]
        public StudentDetailsViewModel StudentDetailsViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public int StudentId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StudentDetailsViewModel.OnInitializedAsync(StudentId.ToString());
        }

        public void Close()
        {
            MudDialog.Cancel();
        }
    }
}
