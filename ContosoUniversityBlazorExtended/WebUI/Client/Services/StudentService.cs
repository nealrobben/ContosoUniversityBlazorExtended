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

    public class StudentService : ServiceBase<StudentsOverviewVM>, IStudentService
    {
        protected override string ControllerName => "students";

        public StudentService(HttpClient http) : base(http)
        {
        }

        public async Task<StudentDetailsVM> GetAsync(string id)
        {
            return await _http.GetFromJsonAsync<StudentDetailsVM>($"{Endpoint}/{id}");
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{Endpoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateStudentCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(Endpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateStudentCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(Endpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<StudentsForCourseVM> GetStudentsForCourse(string courseId)
        {
            return await _http.GetFromJsonAsync<StudentsForCourseVM>($"{Endpoint}/bycourse/{courseId}");
        }
    }
}
