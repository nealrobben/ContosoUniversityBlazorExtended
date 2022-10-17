using MediatR;
using WebUI.Shared.Home.Queries.GetAboutInfo;

namespace ContosoUniversityBlazor.Application.Home.Queries.GetAboutInfo;

public class GetAboutInfoQuery : IRequest<AboutInfoVM>
{
}
