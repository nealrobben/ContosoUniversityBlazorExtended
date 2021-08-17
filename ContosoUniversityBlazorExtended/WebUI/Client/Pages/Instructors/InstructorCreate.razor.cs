using Microsoft.AspNetCore.Components;
using WebUI.Client.ViewModels.Instructors;

namespace WebUI.Client.Pages.Instructors
{
    public partial class InstructorCreate
    {
        [Inject]
        public InstructorCreateViewModel InstructorCreateViewModel { get; set; }
    }
}
