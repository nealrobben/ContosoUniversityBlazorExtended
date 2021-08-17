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
    public class InstructorService : ServiceBase
    {
        public InstructorService(HttpClient http) : base(http)
        {
        }

        public async Task<InstructorsOverviewVM> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<InstructorsOverviewVM>("/api/instructors");
        }

        public async Task<InstructorDetailsVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<InstructorDetailsVM>($"/api/instructors/{id}");
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _http.DeleteAsync($"/api/instructors/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateInstructorCommand createCommand)
        {
            return await _http.PostAsJsonAsync($"/api/instructors", createCommand);
        }

        public async Task<HttpResponseMessage> UpdateAsync(UpdateInstructorCommand createCommand)
        {
            return await _http.PutAsJsonAsync($"/api/instructors", createCommand);
        }

        public async Task<InstructorsLookupVM> GetLookupAsync()
        {
            return await _http.GetFromJsonAsync<InstructorsLookupVM>("/api/instructors/lookup");
        }
    }
}
