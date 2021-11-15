using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorDetailsViewModel : InstructorViewModelBase
    {
        private string _id;

        public InstructorDetailsVM Instructor { get; set; }

        public InstructorDetailsViewModel(IInstructorService instructorService, 
            IStringLocalizer<InstructorResources> instructorLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            :base(instructorService, instructorLocalizer, generalLocalizer)
        {
        }

        public async Task OnInitializedAsync(string id)
        {
            _id = id;
            Instructor = await _instructorService.GetAsync(id);
        }
    }
}
