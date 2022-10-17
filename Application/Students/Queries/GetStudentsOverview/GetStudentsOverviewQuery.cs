using MediatR;
using WebUI.Shared.Common;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace ContosoUniversityBlazor.Application.Students.Queries.GetStudentsOverview;

public class GetStudentsOverviewQuery : IRequest<OverviewVM<StudentOverviewVM>>
{
    public string SortOrder { get; set; }
    public string SearchString { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public GetStudentsOverviewQuery(string sortOrder,
        string searchString, int? pageNumber, int? pageSize)
    {
        SortOrder = sortOrder;
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
