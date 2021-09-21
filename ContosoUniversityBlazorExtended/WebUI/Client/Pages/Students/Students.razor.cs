using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Client.ViewModels.Students;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.Client.Pages.Students
{
    public partial class Students
    {
        [Inject]
        public StudentsViewModel StudentsViewModel { get; set; }

        private MudTable<StudentOverviewVM> table;

        protected override void OnAfterRender(bool firstRender)
        {
            StudentsViewModel.Table = table;
        }
    }
}
