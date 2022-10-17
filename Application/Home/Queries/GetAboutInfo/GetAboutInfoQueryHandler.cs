using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebUI.Shared.Home.Queries.GetAboutInfo;

namespace ContosoUniversityBlazor.Application.Home.Queries.GetAboutInfo;

public class GetAboutInfoQueryHandler : IRequestHandler<GetAboutInfoQuery, AboutInfoVM>
{
    private readonly ISchoolContext _context;

    public GetAboutInfoQueryHandler(ISchoolContext context)
    {
        _context = context;
    }

    public async Task<AboutInfoVM> Handle(GetAboutInfoQuery request, CancellationToken cancellationToken)
    {
        var data = from student in _context.Students
                   group student by student.EnrollmentDate into dateGroup
                   select new EnrollmentDateGroup
                   {
                       EnrollmentDate = dateGroup.Key,
                       StudentCount = dateGroup.Count()
                   };

        return new AboutInfoVM(await data.AsNoTracking().ToListAsync());
    }
}
