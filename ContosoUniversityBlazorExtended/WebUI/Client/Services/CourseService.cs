﻿using System.Net.Http;
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

    public class CourseService : ServiceBase<CoursesOverviewVM, CourseDetailVM>, ICourseService
    {
        protected override string ControllerName => "courses";

        public CourseService(HttpClient http) : base(http)
        {
        }

        public async Task DeleteAsync(string id)
        {
            var result = await _http.DeleteAsync($"{Endpoint}/{id}");

            result.EnsureSuccessStatusCode();
        }

        public async Task CreateAsync(CreateCourseCommand createCommand)
        {
            var result = await _http.PostAsJsonAsync(Endpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task UpdateAsync(UpdateCourseCommand createCommand)
        {
            var result = await _http.PutAsJsonAsync(Endpoint, createCommand);

            result.EnsureSuccessStatusCode();
        }

        public async Task<CoursesForInstructorOverviewVM> GetCoursesForInstructor(string instructorId)
        {
            return await _http.GetFromJsonAsync<CoursesForInstructorOverviewVM>($"{Endpoint}/byinstructor/{instructorId}");
        }
    }
}
