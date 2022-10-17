using MediatR;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsLookup;

public class GetDepartmentsLookupQuery : IRequest<DepartmentsLookupVM>
{
}
