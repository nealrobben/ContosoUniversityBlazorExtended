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

    public class InstructorService : ServiceBase, IInstructorService
    {
        public InstructorService(HttpClient http) : base(http)
        {
        }

        public async Task<InstructorsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = "/api/instructors";

            if (pageNumber != null)
            {
                url += $"?pageNumber={pageNumber}";
            }

            if (!string.IsNullOrEmpty(searchString))
            {
                if (!url.Contains("?"))
                    url += $"?searchString={searchString}";
                else
                    url += $"&searchString={searchString}";
            }

            if (!string.IsNullOrEmpty(sortOrder))
            {
                if (!url.Contains("?"))
                    url += $"?sortOrder={sortOrder}";
                else
                    url += $"&sortOrder={sortOrder}";
            }

            if (pageSize != null)
            {
                if (!url.Contains("?"))
                    url += $"?pageSize={pageSize}";
                else
                    url += $"&pageSize={pageSize}";
            }

            return await _http.GetFromJsonAsync<InstructorsOverviewVM>(url);
        }

        public async Task<InstructorDetailsVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<InstructorDetailsVM>($"/api/instructors/{id}");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"/api/instructors/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateInstructorCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync($"/api/instructors", createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateInstructorCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync($"/api/instructors", createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<InstructorsLookupVM> GetLookupAsync()
        {
            return await _http.GetFromJsonAsync<InstructorsLookupVM>("/api/instructors/lookup");
        }
    }
}
