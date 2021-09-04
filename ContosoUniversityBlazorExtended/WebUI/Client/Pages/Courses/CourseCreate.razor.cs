using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseCreate
    {
        [Inject]
        public CourseCreateViewModel CourseCreateViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CourseCreateViewModel.OnInitializedAsync(MudDialog);
        }
    }
}
