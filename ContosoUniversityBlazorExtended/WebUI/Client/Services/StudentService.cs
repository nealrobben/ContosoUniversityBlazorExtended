using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Students.Commands.CreateStudent;
using WebUI.Shared.Students.Commands.UpdateStudent;
using WebUI.Shared.Students.Queries.GetStudentDetails;
using WebUI.Shared.Students.Queries.GetStudentsOverview;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace WebUI.Client.Services
{
    public class StudentService : ServiceBase
    {
        public StudentService(HttpClient http) : base(http)
        {
        }

        public async Task<StudentsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = "/api/students";

            if(pageNumber != null)
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

            if(pageSize != null)
            {
                if (!url.Contains("?"))
                    url += $"?pageSize={pageSize}";
                else
                    url += $"&pageSize={pageSize}";
            }

            return await _http.GetFromJsonAsync<StudentsOverviewVM>(url);
        }

        public async Task<StudentDetailsVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<StudentDetailsVM>($"/api/students/{id}");
        }

        public async Task<HttpResponseMessage> DeleteAsync(string id)
        {
            return await _http.DeleteAsync($"/api/students/{id}");
        }

        public async Task<HttpResponseMessage> CreateAsync(CreateStudentCommand createCommand)
        {
            return await _http.PostAsJsonAsync($"/api/students", createCommand);
        }

        public async Task<HttpResponseMessage> UpdateAsync(UpdateStudentCommand createCommand)
        {
            return await _http.PutAsJsonAsync($"/api/students", createCommand);
        }

        public async Task<StudentsForCourseVM> GetStudentsForCourse(string courseId)
        {
            return await _http.GetFromJsonAsync<StudentsForCourseVM>($"/api/students/bycourse/{courseId}");
        }
    }
}
