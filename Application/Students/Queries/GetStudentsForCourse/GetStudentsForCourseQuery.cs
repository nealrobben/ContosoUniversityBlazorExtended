using MediatR;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace ContosoUniversityBlazor.Application.Students.Queries.GetStudentsForCourse;

public class GetStudentsForCourseQuery : IRequest<StudentsForCourseVM>
{
    public int? ID { get; set; }

    public GetStudentsForCourseQuery(int? id)
    {
        ID = id;
    }
}
