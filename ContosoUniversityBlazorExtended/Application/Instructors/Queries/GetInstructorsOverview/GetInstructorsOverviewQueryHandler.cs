using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsOverview;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace ContosoUniversityBlazor.Application.Instructors.Queries.GetInstructorsOverview
{
    public class GetInstructorsOverviewQueryHandler : IRequestHandler<GetInstructorsOverviewQuery, InstructorsOverviewVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetInstructorsOverviewQueryHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InstructorsOverviewVM> Handle(GetInstructorsOverviewQuery request, CancellationToken cancellationToken)
        {
            return new InstructorsOverviewVM(await GetInstructors(cancellationToken));
        }

        private async Task<List<InstructorVM>> GetInstructors(CancellationToken cancellationToken)
        {
            return await _context.Instructors
                  .Include(i => i.OfficeAssignment)
                  .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Enrollments)
                            .ThenInclude(i => i.Student)
                  .Include(i => i.CourseAssignments)
                    .ThenInclude(i => i.Course)
                        .ThenInclude(i => i.Department)
                  .AsNoTracking()
                  .OrderBy(i => i.LastName)
                  .ProjectTo<InstructorVM>(_mapper.ConfigurationProvider)
                  .ToListAsync(cancellationToken);
        }
    }
}
