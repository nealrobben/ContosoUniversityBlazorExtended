using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseDetails
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public CourseDetailsViewModel CourseDetailsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CourseDetailsViewModel.OnInitializedAsync(id);
        }
    }
}
