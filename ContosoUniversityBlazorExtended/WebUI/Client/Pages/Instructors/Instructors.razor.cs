using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Client.ViewModels.Instructors;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace WebUI.Client.Pages.Instructors
{
    public partial class Instructors
    {
        [Inject]
        public InstructorsViewModel InstructorsViewModel { get; set; }

        private MudTable<InstructorVM> table;

        protected override void OnAfterRender(bool firstRender)
        {
            InstructorsViewModel.Table = table;
        }
    }
}
