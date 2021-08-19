using MediatR;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview
{
    public class GetInstructorsOverviewQuery : IRequest<InstructorsOverviewVM>
    {
    }
}
