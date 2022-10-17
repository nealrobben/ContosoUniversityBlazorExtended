using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.CreateDepartment;

namespace ContosoUniversityBlazor.Application.Departments.Commands.CreateDepartment;

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, int>
{
    private readonly ISchoolContext _context;

    public CreateDepartmentCommandHandler(ISchoolContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var newDepartment = new Department
        {
            Name = request.Name,
            Budget = request.Budget,
            StartDate = request.StartDate,
            InstructorID = request.InstructorID
        };

        _context.Departments.Add(newDepartment);

        await _context.SaveChangesAsync(cancellationToken);

        return newDepartment.DepartmentID;
    }
}
