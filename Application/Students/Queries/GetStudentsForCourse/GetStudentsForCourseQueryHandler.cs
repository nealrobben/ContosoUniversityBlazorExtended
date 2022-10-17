using AutoMapper;
using AutoMapper.QueryableExtensions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace ContosoUniversityBlazor.Application.Students.Queries.GetStudentsForCourse;

public class GetStudentsForCourseQueryHandler : IRequestHandler<GetStudentsForCourseQuery, StudentsForCourseVM>
{
    private readonly ISchoolContext _context;
    private readonly IMapper _mapper;

    public GetStudentsForCourseQueryHandler(ISchoolContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<StudentsForCourseVM> Handle(GetStudentsForCourseQuery request, CancellationToken cancellationToken)
    {
        if (request.ID == null)
            return new StudentsForCourseVM(new List<StudentForCourseVM>());

        var students = await _context.Enrollments
            .Where(x => x.CourseID == request.ID)
            .Include(c => c.Student)
            .AsNoTracking()
            .ProjectTo<StudentForCourseVM>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new StudentsForCourseVM(students);
    }
}
