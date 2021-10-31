using Microsoft.Extensions.Localization;
using WebUI.Client.Services;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorViewModelBase
    {
        protected readonly InstructorService _instructorService;

        protected readonly IStringLocalizer<InstructorResources> _instructorLocalizer;
        protected readonly IStringLocalizer<GeneralResources> _generalLocalizer;

        public InstructorViewModelBase(InstructorService instructorService, IStringLocalizer<InstructorResources> instructorLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
        {
            _instructorService = instructorService;
            _instructorLocalizer = instructorLocalizer;
            _generalLocalizer = generalLocalizer;
        }
    }
}
