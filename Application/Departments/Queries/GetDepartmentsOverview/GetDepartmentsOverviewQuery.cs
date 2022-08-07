using MediatR;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsOverview
{
    public class GetDepartmentsOverviewQuery : IRequest<DepartmentsOverviewVM>
    {
        public string SortOrder { get; set; }
        public string SearchString { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public GetDepartmentsOverviewQuery(string sortOrder,
            string searchString, int? pageNumber, int? pageSize)
        {
            SortOrder = sortOrder;
            SearchString = searchString;
            PageNumber = pageNumber;
            PageSize = pageSize;
        }
    }
}
