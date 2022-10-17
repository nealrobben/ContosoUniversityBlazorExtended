using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Common;
using WebUI.Shared.Instructors.Commands.CreateInstructor;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace WebUI.Client.Services;

public interface IInstructorService 
    : IServiceBase<OverviewVM<InstructorVM>, InstructorDetailsVM,
        CreateInstructorCommand, UpdateInstructorCommand>
{
    Task<InstructorsLookupVM> GetLookupAsync();
}

public class InstructorService 
    : ServiceBase<OverviewVM<InstructorVM>, InstructorDetailsVM, 
        CreateInstructorCommand, UpdateInstructorCommand>, IInstructorService
{
    public InstructorService(HttpClient http) 
        : base(http, "instructors")
    {
    }

    public async Task<InstructorsLookupVM> GetLookupAsync()
    {
        return await _http.GetFromJsonAsync<InstructorsLookupVM>($"{Endpoint}/lookup");
    }
}
