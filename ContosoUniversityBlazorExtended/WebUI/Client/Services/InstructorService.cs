using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Instructors.Commands.CreateInstructor;
using WebUI.Shared.Instructors.Commands.UpdateInstructor;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace WebUI.Client.Services
{
    public interface IInstructorService
    {
        Task CreateAsync(CreateInstructorCommand createCommand);
        Task DeleteAsync(string id);
        Task<InstructorsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize);
        Task<InstructorDetailsVM> GetAsync(string id);
        Task<InstructorsLookupVM> GetLookupAsync();
        Task UpdateAsync(UpdateInstructorCommand createCommand);
    }

    public class InstructorService : ServiceBase<InstructorsOverviewVM, InstructorDetailsVM>, IInstructorService
    {
        protected override string ControllerName => "instructors";

        public InstructorService(HttpClient http) : base(http)
        {
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{Endpoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateInstructorCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(Endpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateInstructorCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(Endpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<InstructorsLookupVM> GetLookupAsync()
        {
            return await _http.GetFromJsonAsync<InstructorsLookupVM>($"{Endpoint}/lookup");
        }
    }
}
