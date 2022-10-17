using Application.Common.Extensions;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Common;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview;

public class GetCoursesOverviewQueryHandler : IRequestHandler<GetCoursesOverviewQuery, OverviewVM<CourseVM>>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    private const int _defaultPageSize = 10;

    public GetCoursesOverviewQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OverviewVM<CourseVM>> Handle(GetCoursesOverviewQuery request, CancellationToken cancellationToken)
    {
        var courses = _context.Courses
            .Search(request.SearchString)
            .Sort(request.SortOrder);

        var totalCourses = await courses.CountAsync();

        var metaData = new MetaData(request.PageNumber ?? 0, totalCourses,
            request.PageSize ?? _defaultPageSize, request.SortOrder, request.SearchString);

        var items = await courses
            .Include(c => c.Department)
            .AsNoTracking()
            .Skip((metaData.PageNumber) * metaData.PageSize)
            .Take(metaData.PageSize)
            .ProjectTo<CourseVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OverviewVM<CourseVM>(items, metaData);
    }
}
