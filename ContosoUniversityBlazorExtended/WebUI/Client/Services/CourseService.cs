using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.Client.Services
{
    public interface ICourseService
    {
        Task CreateAsync(CreateCourseCommand createCommand);
        Task DeleteAsync(string id);
        Task<CoursesOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize);
        Task<CourseDetailVM> GetAsync(string id);
        Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId);
        Task UpdateAsync(UpdateCourseCommand createCommand);
    }

    public class CourseService : ServiceBase, ICourseService
    {
        private const string _coursesEndpoint = "/api/courses";

        public CourseService(HttpClient http) : base(http)
        {
        }

        public async Task<CoursesOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = _coursesEndpoint;

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

            return await _http.GetFromJsonAsync<CoursesOverviewVM>(url);
        }

        public async Task<CourseDetailVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<CourseDetailVM>($"{_coursesEndpoint}/{id}");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{_coursesEndpoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateCourseCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(_coursesEndpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateCourseCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(_coursesEndpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId)
        {
            return await _http.GetFromJsonAsync<CoursesForInstructorOverviewVM>($"{_coursesEndpoint}/byinstructor/{instructorId}");
        }
    }
}
