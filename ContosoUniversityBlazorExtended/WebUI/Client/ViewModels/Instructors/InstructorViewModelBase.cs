using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorViewModelBase
    {
        protected readonly InstructorService _instructorService;

        public InstructorViewModelBase(InstructorService instructorService)
        {
            _instructorService = instructorService;
        }
    }
}
