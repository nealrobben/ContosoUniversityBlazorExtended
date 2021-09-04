using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorCreate
    {
        [Inject]
        public InstructorCreateViewModel InstructorCreateViewModel { get; set; }

        [CascadingParameter]
        MudDialogInstance MudDialog { get; set; }

        protected override void OnInitialized()
        {
            InstructorCreateViewModel.OnInitialized(MudDialog);
        }
    }
}
