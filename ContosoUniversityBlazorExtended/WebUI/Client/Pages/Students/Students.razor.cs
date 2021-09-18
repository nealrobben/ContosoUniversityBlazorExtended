using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class Students
    {
        [Inject]
        public StudentsViewModel StudentsViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StudentsViewModel.OnInitializedAsync();
        }
    }
}
