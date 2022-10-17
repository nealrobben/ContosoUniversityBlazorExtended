using MediatR;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;

namespace ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview;

public class GetCoursesForInstructorQuery : IRequest<CoursesForInstructorOverviewVM>
{
    public int? ID { get; set; }

    public GetCoursesForInstructorQuery(int? id)
    {
        ID = id;
    }
}
