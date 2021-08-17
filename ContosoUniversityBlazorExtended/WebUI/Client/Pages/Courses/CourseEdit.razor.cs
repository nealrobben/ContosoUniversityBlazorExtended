using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class CourseEdit
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public CourseEditViewModel CourseEditViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CourseEditViewModel.OnInitializedAsync(id);
        }
    }
}
