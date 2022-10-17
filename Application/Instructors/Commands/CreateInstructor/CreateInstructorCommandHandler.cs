using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace ContosoUniversityBlazor.Application.Instructors.Commands.CreateInstructor;

public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand,int>
{
    private readonly ISchoolContext _context;

    public CreateInstructorCommandHandler(ISchoolContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
    {
        var newInstructor = new Instructor
        {
            FirstMidName = request.FirstName,
            LastName = request.LastName,
            HireDate = request.HireDate,
            ProfilePictureName = request.ProfilePictureName
        };
        _context.Instructors.Add(newInstructor);

        await _context.SaveChangesAsync(cancellationToken);

        return newInstructor.ID;
    }
}
