using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Students
{
    public class StudentViewModelBase
    {
        protected readonly StudentService _studentService;

        public StudentViewModelBase(StudentService studentService)
        {
            _studentService = studentService;
        }
    }
}
