using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseEdit
    {
        [Parameter]
        public string CourseId { get; set; }

        [Inject]
        public CourseEditViewModel CourseEditViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CourseEditViewModel.OnInitializedAsync(CourseId, MudDialog);
        }
    }
}
