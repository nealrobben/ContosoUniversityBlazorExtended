using MediatR;
using WebUI.Shared.Common;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview;

public class GetCoursesOverviewQuery : IRequest<OverviewVM<CourseVM>>
{
    public string SortOrder { get; set; }
    public string SearchString { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public GetCoursesOverviewQuery(string sortOrder,
        string searchString, int? pageNumber, int? pageSize)
    {
        SortOrder = sortOrder;
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
