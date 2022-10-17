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
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;

namespace ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsOverview;

public class GetDepartmentsOverviewQueryHandler : IRequestHandler<GetDepartmentsOverviewQuery, OverviewVM<DepartmentVM>>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    private const int _defaultPageSize = 10;

    public GetDepartmentsOverviewQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OverviewVM<DepartmentVM>> Handle(GetDepartmentsOverviewQuery request, CancellationToken cancellationToken)
    {
        var departments = _context.Departments
            .Search(request.SearchString)
            .Sort(request.SortOrder);

        var totalDepartments = await departments.CountAsync();

        var metaData = new MetaData(request.PageNumber ?? 0, totalDepartments,
            request.PageSize ?? _defaultPageSize, request.SortOrder, request.SearchString);

        var items = await departments
            .Include(d => d.Administrator)
            .AsNoTracking()
            .Skip((metaData.PageNumber) * metaData.PageSize)
            .Take(metaData.PageSize)
            .ProjectTo<DepartmentVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new OverviewVM<DepartmentVM>(items, metaData);
    }
}
