using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace ContosoUniversityBlazor.Application.Students.Commands.CreateStudent;

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand,int>
{
    private readonly ISchoolContext _context;

    public CreateStudentCommandHandler(ISchoolContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {
        var newStudent = new Student
        {
            FirstMidName = request.FirstName,
            LastName = request.LastName,
            EnrollmentDate = request.EnrollmentDate,
            ProfilePictureName = request.ProfilePictureName
        };

        _context.Students.Add(newStudent);

        await _context.SaveChangesAsync(cancellationToken);

        return newStudent.ID;
    }
}
