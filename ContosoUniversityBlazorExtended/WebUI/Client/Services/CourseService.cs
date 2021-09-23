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
    public class CourseService : ServiceBase
    {
        public CourseService(HttpClient http) : base(http)
        {
        }

        public async Task<CoursesOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = "/api/courses";

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
            return await _http.GetFromJsonAsync<CourseDetailVM>($"/api/courses/{id}");
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _http.DeleteAsync($"/api/courses/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateCourseCommand createCommand)
        {
            return await _http.PostAsJsonAsync($"/api/courses", createCommand);
        }

        public async Task<HttpResponseMessage> UpdateAsync(UpdateCourseCommand createCommand)
        {
            return await _http.PutAsJsonAsync($"/api/courses", createCommand);
        }

        public async Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId)
        {
            return await _http.GetFromJsonAsync<CoursesForInstructorOverviewVM>($"/api/courses/byinstructor/{instructorId}");
        }
    }
}
