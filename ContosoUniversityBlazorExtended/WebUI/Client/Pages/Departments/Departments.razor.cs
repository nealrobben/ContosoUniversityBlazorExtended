using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Client.ViewModels.Departments;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace WebUI.Client.Pages.Departments
{
    public partial class Departments
    {
        [Inject]
        public DepartmentsViewModel DepartmentsViewModel { get; set; }

        private MudTable<DepartmentVM> table;

        protected override void OnAfterRender(bool firstRender)
        {
            DepartmentsViewModel.Table = table;
        }
    }
}
