using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Exceptions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;

namespace ContosoUniversityBlazor.Application.Instructors.Queries.GetInstructorDetails;

public class GetInstructorDetailsQueryHandler : IRequestHandler<GetInstructorDetailsQuery, InstructorDetailsVM>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    public GetInstructorDetailsQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<InstructorDetailsVM> Handle(GetInstructorDetailsQuery request, CancellationToken cancellationToken)
    {
        if (request.ID == null)
            throw new NotFoundException(nameof(Instructor), request.ID);

        var instructor = await _context.Instructors
            .AsNoTracking()
            .ProjectTo<InstructorDetailsVM>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(m => m.InstructorID == request.ID, cancellationToken);

        if (instructor == null)
            throw new NotFoundException(nameof(Instructor), request.ID);

        return instructor;
    }
}
