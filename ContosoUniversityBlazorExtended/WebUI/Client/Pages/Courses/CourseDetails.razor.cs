using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseDetails
    {
        [Inject]
        public CourseDetailsViewModel CourseDetailsViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        [Parameter]
        public int CourseId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CourseDetailsViewModel.OnInitializedAsync(CourseId.ToString());
        }

        public void Close()
        {
            MudDialog.Cancel();
        }
    }
}
