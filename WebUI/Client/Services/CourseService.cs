using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebUI.Shared.Common;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace WebUI.Client.Services;

public interface ICourseService 
    : IServiceBase<OverviewVM<CourseVM>, CourseDetailVM,
        CreateCourseCommand, UpdateCourseCommand>
{
    Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId);
}

public class CourseService 
    : ServiceBase<OverviewVM<CourseVM>, CourseDetailVM, 
        CreateCourseCommand, UpdateCourseCommand>, ICourseService
{
    public CourseService(HttpClient http) 
        : base(http, "courses")
    {
    }

    public async Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId)
    {
        return await _http.GetFromJsonAsync<CoursesForInstructorOverviewVM>($"{Endpoint}/byinstructor/{instructorId}");
    }
}
