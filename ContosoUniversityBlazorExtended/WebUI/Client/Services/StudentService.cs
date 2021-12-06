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
    public interface IStudentService
    {
        Task CreateAsync(CreateStudentCommand createCommand);
        Task DeleteAsync(string id);
        Task<StudentsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize);
        Task<StudentDetailsVM> GetAsync(string id);
        Task<StudentsForCourseVM> GetStudentsForCourse(string courseId);
        Task UpdateAsync(UpdateStudentCommand createCommand);
    }

    public class StudentService : ServiceBase, IStudentService
    {
        private const string _studentsEndpoint = $"{apiBase}/students";

        public StudentService(HttpClient http) : base(http)
        {
        }

        public async Task<StudentsOverviewVM> GetAllAsync(string sortOrder, int? pageNumber, string searchString, int? pageSize)
        {
            var url = _studentsEndpoint;

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

            return await _http.GetFromJsonAsync<StudentsOverviewVM>(url);
        }

        public async Task<StudentDetailsVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<StudentDetailsVM>($"{_studentsEndpoint}/{id}");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{_studentsEndpoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateStudentCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(_studentsEndpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateStudentCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(_studentsEndpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<StudentsForCourseVM> GetStudentsForCourse(string courseId)
        {
            return await _http.GetFromJsonAsync<StudentsForCourseVM>($"{_studentsEndpoint}/bycourse/{courseId}");
        }
    }
}
