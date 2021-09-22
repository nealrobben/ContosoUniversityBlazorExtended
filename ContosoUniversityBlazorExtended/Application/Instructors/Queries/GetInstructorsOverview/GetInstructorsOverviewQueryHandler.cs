using Application.Common.Extensions;
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
using WebUI.Shared.Common;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace ContosoUniversityBlazor.Application.Instructors.Queries.GetInstructorsOverview
{
    public class GetInstructorsOverviewQueryHandler : IRequestHandler<GetInstructorsOverviewQuery, InstructorsOverviewVM>
    {
        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        private const int _defaultPageSize = 10;

        public GetInstructorsOverviewQueryHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<InstructorsOverviewVM> Handle(GetInstructorsOverviewQuery request, CancellationToken cancellationToken)
        {
            var instructors = _context.Instructors
                .Search(request.SearchString)
                .Sort(request.SortOrder);

            var totalInstructors = await instructors.CountAsync();

            var metaData = new MetaData(request.PageNumber ?? 0, totalInstructors,
                request.PageSize ?? _defaultPageSize, request.SortOrder, request.SearchString);

            var items = await instructors
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

            return new InstructorsOverviewVM(items, metaData);
        }
    }
}
