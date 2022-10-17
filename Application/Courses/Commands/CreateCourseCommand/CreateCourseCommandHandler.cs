using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Commands.CreateCourse;

namespace ContosoUniversityBlazor.Application.Courses.Commands.CreateCourse;

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand,int>
{
    private readonly ISchoolContext _context;

    public CreateCourseCommandHandler(ISchoolContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            CourseID = request.CourseID,
            Title = request.Title,
            Credits = request.Credits,
            DepartmentID = request.DepartmentID
        };

        _context.Courses.Add(course);
        await _context.SaveChangesAsync(cancellationToken);

        return course.CourseID;
    }
}
