using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentDetails
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public StudentDetailsViewModel StudentDetailsViewModel { get; set; }

        [Inject]
        public StudentService StudentService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StudentDetailsViewModel.OnInitializedAsync(id);
        }
    }
}
