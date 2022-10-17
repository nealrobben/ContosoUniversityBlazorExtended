using MediatR;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace ContosoUniversityBlazor.Application.Instructors.Queries.GetInstructorsLookup;

public class GetInstructorLookupQuery : IRequest<InstructorsLookupVM>
{
}
