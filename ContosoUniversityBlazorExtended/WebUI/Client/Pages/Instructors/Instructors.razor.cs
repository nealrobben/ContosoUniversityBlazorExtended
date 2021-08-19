using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class Instructors
    {
        [Inject]
        public InstructorsViewModel InstructorsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await InstructorsViewModel.OnInitializedAsync();
        }
    }
}
