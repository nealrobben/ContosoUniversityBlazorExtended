using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseCreate
    {
        [Inject]
        public CourseCreateViewModel CourseCreateViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CourseCreateViewModel.OnInitializedAsync();
        }
    }
}
