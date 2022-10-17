using MediatR;
using WebUI.Shared.Common;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview;

public class GetInstructorsOverviewQuery : IRequest<OverviewVM<InstructorVM>>
{
    public string SortOrder { get; set; }
    public string SearchString { get; set; }
    public int? PageNumber { get; set; }
    public int? PageSize { get; set; }

    public GetInstructorsOverviewQuery(string sortOrder,
        string searchString, int? pageNumber, int? pageSize)
    {
        SortOrder = sortOrder;
        SearchString = searchString;
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}
