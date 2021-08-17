using ContosoUniversityBlazor.Application.Common.Exceptions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityBlazor.Application.Students.Commands.DeleteStudent
{
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand>
    {
        private readonly ISchoolContext _context;

        public DeleteStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var student = await _context.Students.FindAsync(request.ID);

            if (student == null)
                throw new NotFoundException(nameof(Student), request.ID);

            _context.Students.Remove(student);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
