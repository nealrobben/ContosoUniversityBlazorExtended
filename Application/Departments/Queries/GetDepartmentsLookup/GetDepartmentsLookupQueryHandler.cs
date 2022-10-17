using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;

namespace ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsLookup;

public class GetDepartmentsLookupQueryHandler : IRequestHandler<GetDepartmentsLookupQuery, DepartmentsLookupVM>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsLookupQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartmentsLookupVM> Handle(GetDepartmentsLookupQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.Departments
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ProjectTo<DepartmentLookupVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new DepartmentsLookupVM(list);
    }
}
