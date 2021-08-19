using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Courses;

namespace WebUI.Client.Pages.Courses
{
    public partial class Courses
    {
        [Inject]
        public CoursesViewModel CoursesViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await CoursesViewModel.Initialize();
        }
    }
}
