using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorDetails
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public InstructorDetailsViewModel InstructorDetailsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await InstructorDetailsViewModel.OnInitializedAsync(id);
        }
    }
}
