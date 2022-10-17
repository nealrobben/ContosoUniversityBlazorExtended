using ContosoUniversityBlazor.Application.Common.Exceptions;
using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.UpdateDepartment;

namespace ContosoUniversityBlazor.Application.Departments.Commands.UpdateDepartment;

public class UpdateDepartmentCommandHandler : IRequestHandler<UpdateDepartmentCommand>
{
    private readonly ISchoolContext _context;

    public UpdateDepartmentCommandHandler(ISchoolContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
    {
        if (request.DepartmentID == 0)
            throw new NotFoundException(nameof(Department),request.DepartmentID);

        var departmentToUpdate = await _context.Departments
            .Include(i => i.Administrator)
            .FirstOrDefaultAsync(m => m.DepartmentID == request.DepartmentID);

        if (departmentToUpdate == null)
            throw new NotFoundException(nameof(Department), request.DepartmentID);

        departmentToUpdate.Name = request.Name;
        departmentToUpdate.Budget = request.Budget;
        departmentToUpdate.StartDate = request.StartDate;
        departmentToUpdate.InstructorID = request.InstructorID;

        _context.Entry<Department>(departmentToUpdate).Property("RowVersion").OriginalValue = request.RowVersion;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
