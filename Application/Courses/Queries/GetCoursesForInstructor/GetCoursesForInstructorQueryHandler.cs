using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;

namespace ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview;

public class GetCoursesForInstructorQueryHandler : IRequestHandler<GetCoursesForInstructorQuery, CoursesForInstructorOverviewVM>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    public GetCoursesForInstructorQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CoursesForInstructorOverviewVM> Handle(GetCoursesForInstructorQuery request, CancellationToken cancellationToken)
    {
        if (request.ID == null)
            return new CoursesForInstructorOverviewVM(new List<CourseForInstructorVM>());

        var courseIdsForInstructor = await _context.CourseAssignments
            .Where(x => x.InstructorID == request.ID)
            .Select(x => x.CourseID)
            .ToListAsync(cancellationToken);

        var courses = await _context.Courses
            .Where(x => courseIdsForInstructor.Contains(x.CourseID))
            .Include(c => c.Department)
            .AsNoTracking()
            .ProjectTo<CourseForInstructorVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new CoursesForInstructorOverviewVM(courses);
    }
}
