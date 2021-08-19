using ContosoUniversityBlazor.Application.Common.Exceptions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityBlazor.Application.Instructors.Commands.DeleteInstructor
{
    public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand>
    {
        private readonly ISchoolContext _context;

        public DeleteInstructorCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var instructor = await _context.Instructors
                .Include(i => i.CourseAssignments)
                .SingleAsync(i => i.ID == request.ID);

            if (instructor == null)
                throw new NotFoundException(nameof(Instructor), request.ID);

            var departments = await _context.Departments
                .Where(d => d.InstructorID == request.ID)
                .ToListAsync();
            departments.ForEach(d => d.InstructorID = null);

            _context.Instructors.Remove(instructor);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
