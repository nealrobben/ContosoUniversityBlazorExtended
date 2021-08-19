using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentEdit
    {
        [Parameter]
        public string id { get; set; }

        [Inject]
        public StudentEditViewModel StudentEditViewModel { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await StudentEditViewModel.OnInitializedAsync(id);
        }
    }
}
