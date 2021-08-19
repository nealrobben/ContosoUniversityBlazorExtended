using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview
{
    public class GetCoursesOverviewQueryHandler : IRequestHandler<GetCoursesOverviewQuery, CoursesOverviewVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetCoursesOverviewQueryHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CoursesOverviewVM> Handle(GetCoursesOverviewQuery request, CancellationToken cancellationToken)
        {
            var courses = await _context.Courses
                .Include(c => c.Department)
                .AsNoTracking()
                .ProjectTo<CourseVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new CoursesOverviewVM(courses);
        }
    }
}
