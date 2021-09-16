using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace ContosoUniversityBlazor.Application.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand>
    {
        private readonly ISchoolContext _context;

        public CreateStudentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            _context.Students.Add(new Student 
            {
                FirstMidName = request.FirstName,
                LastName = request.LastName,
                EnrollmentDate = request.EnrollmentDate,
                ProfilePictureName = request.ProfilePictureName
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
