using ContosoUniversityBlazor.Application.Common.Exceptions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Students.Commands.UpdateStudent;

namespace ContosoUniversityBlazor.Application.Students.Commands.UpdateStudent
{
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand>
    {
        private readonly ISchoolContext _context;

        public UpdateStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.StudentID == null)
                throw new NotFoundException(nameof(Student), request.StudentID);

            var studentToUpdate = await _context.Students
                .FirstOrDefaultAsync(s => s.ID == request.StudentID);

            studentToUpdate.FirstMidName = request.FirstName;
            studentToUpdate.LastName = request.LastName;
            studentToUpdate.EnrollmentDate = request.EnrollmentDate;

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
