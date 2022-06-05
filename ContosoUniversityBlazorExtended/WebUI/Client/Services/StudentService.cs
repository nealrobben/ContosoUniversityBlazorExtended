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
        : IServiceBase<StudentsOverviewVM, StudentDetailsVM, 
            CreateStudentCommand, UpdateStudentCommand>
    {
        Task<StudentsForCourseVM> GetStudentsForCourse(string courseId);
    }

    public class StudentService 
        : ServiceBase<StudentsOverviewVM, StudentDetailsVM, 
            CreateStudentCommand, UpdateStudentCommand>, IStudentService
    {
        protected override string ControllerName => "students";

        public StudentService(HttpClient http) : base(http)
        {
        }

        public async Task<StudentsForCourseVM> GetStudentsForCourse(string courseId)
        {
            return await _http.GetFromJsonAsync<StudentsForCourseVM>($"{Endpoint}/bycourse/{courseId}");
        }
    }
}
