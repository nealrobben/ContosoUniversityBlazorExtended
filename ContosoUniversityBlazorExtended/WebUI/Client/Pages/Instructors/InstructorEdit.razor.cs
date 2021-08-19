using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorEdit
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public InstructorEditViewModel InstructorEditViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await InstructorEditViewModel.OnInitializedAsync(id);
        }
    }
}
