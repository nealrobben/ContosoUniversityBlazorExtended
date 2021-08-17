using MediatR;

namespace WebUI.Shared.Courses.Commands.CreateCourse
{
    public class CreateCourseCommand : IRequest
    {
        public int CourseID { get; set; }

        public string Title { get; set; }

        public int Credits { get; set; }

        public int DepartmentID { get; set; }
    }
}
