using Application.Common.Interfaces;
using ContosoUniversityBlazor.Application.Common.Exceptions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityBlazor.Application.Instructors.Commands.DeleteInstructor;

public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
{
    private readonly ISchoolContext _context;
    private readonly IProfilePictureService _profilePictureService;

    public DeleteInstructorCommandHandler(ISchoolContext context, IProfilePictureService profilePictureService)
    {
        _context = context;
        _profilePictureService = profilePictureService;
    }

    public async Task<Unit> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
    {
        var instructor = await _context.Instructors
            .Include(i => i.CourseAssignments)
            .SingleOrDefaultAsync(i => i.ID == request.ID);

        if (instructor == null)
            throw new NotFoundException(nameof(Instructor), request.ID);

        if (!string.IsNullOrWhiteSpace(instructor.ProfilePictureName))
            _profilePictureService.DeleteImageFile(instructor.ProfilePictureName);

        var departments = await _context.Departments
            .Where(d => d.InstructorID == request.ID)
            .ToListAsync();
        departments.ForEach(d => d.InstructorID = null);

        _context.Instructors.Remove(instructor);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
