using Microsoft.Extensions.Localization;
using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentViewModelBase
    {
        protected readonly StudentService _studentService;

        protected readonly IStringLocalizer<StudentResources> _studentLocalizer;
        protected readonly IStringLocalizer<GeneralResources> _generalLocalizer;

        public StudentViewModelBase(StudentService studentService, IStringLocalizer<StudentResources> studentLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
        {
            _studentService = studentService;
            _studentLocalizer = studentLocalizer;
            _generalLocalizer = generalLocalizer;
        }
    }
}
