using MediatR;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsOverview
{
    public class GetDepartmentsOverviewQuery : IRequest<DepartmentsOverviewVM>
    {
    }
}
