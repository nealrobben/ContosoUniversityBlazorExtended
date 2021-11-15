using Microsoft.Extensions.Localization;
using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentViewModelBase
    {
        protected readonly IStudentService _studentService;

        protected readonly IStringLocalizer<StudentResources> _studentLocalizer;
        protected readonly IStringLocalizer<GeneralResources> _generalLocalizer;

        public StudentViewModelBase(IStudentService studentService, IStringLocalizer<StudentResources> studentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
        {
            _studentService = studentService;
            _studentLocalizer = studentLocalizer;
            _generalLocalizer = generalLocalizer;
        }
    }
}
