using Microsoft.AspNetCore.Components;
using MudBlazor;
using WebUI.Client.ViewModels.Courses;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.Client.Pages.Courses
{
    public partial class Courses
    {
        [Inject]
        public CoursesViewModel CoursesViewModel { get; set; }

        private MudTable<CourseVM> table;

        protected override void OnAfterRender(bool firstRender)
        {
            CoursesViewModel.Table = table;
        }
    }
}
