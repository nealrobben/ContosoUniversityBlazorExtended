using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Application.Instructors.Queries.GetInstructorsLookup;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Instructors.Queries.GetInstructorsLookup;

namespace ContosoUniversityCQRS.Application.Instructors.Queries.GetInstructorsLookup;

public class GetInstructorLookupQueryHandler : IRequestHandler<GetInstructorLookupQuery, InstructorsLookupVM>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    public GetInstructorLookupQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InstructorsLookupVM> Handle(GetInstructorLookupQuery request, CancellationToken cancellationToken)
    {
        var instructors = await _context.Instructors
            .AsNoTracking()
            .OrderBy(x => x.LastName)
            .ProjectTo<InstructorLookupVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new InstructorsLookupVM(instructors);
    }
}
