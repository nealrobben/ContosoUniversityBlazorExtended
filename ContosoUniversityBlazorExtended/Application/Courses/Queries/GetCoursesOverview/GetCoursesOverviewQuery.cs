using MediatR;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview
{
    public class GetCoursesOverviewQuery : IRequest<CoursesOverviewVM>
    {
    }
}
