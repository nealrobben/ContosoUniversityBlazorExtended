using Microsoft.AspNetCore.Components;
using WebUI.Client.ViewModels.Students;

namespace WebUI.Client.Pages.Students
{
    public partial class StudentCreate
    {
        [Inject]
        public StudentCreateViewModel StudentCreateViewModel { get; set; }
    }
}
