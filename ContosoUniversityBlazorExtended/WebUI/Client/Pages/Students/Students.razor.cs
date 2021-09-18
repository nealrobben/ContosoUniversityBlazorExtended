using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebUI.Client.ViewModels.Students;
using WebUI.Shared.Students.Queries.GetStudentDetails;

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

        private async Task<TableData<StudentDetailsVM>> ServerReload(TableState state)
        {
            return new TableData<StudentDetailsVM>() { TotalItems = 0, Items = new List<StudentDetailsVM>() };
        }
    }
}
