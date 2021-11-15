using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentDetails;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentDetailsViewModel : StudentViewModelBase
    {
        private string _id;

        public StudentDetailsVM Student { get; set; }

        public StudentDetailsViewModel(IStudentService studentService,
            IStringLocalizer<StudentResources> studentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(studentService,studentLocalizer,generalLocalizer)
        {
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;
            Student = await _studentService.GetAsync(id);
        }
    }
}
