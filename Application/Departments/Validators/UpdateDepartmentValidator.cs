using ContosoUniversityBlazor.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.UpdateDepartment;

namespace Application.Departments.Validators;

public class UpdateDepartmentValidator 
    : WebUI.Shared.Departments.Validators.UpdateDepartmentValidator
{
    private readonly ISchoolContext _context;

    public UpdateDepartmentValidator(ISchoolContext context) : base()
    {
        _context = context;

        RuleFor(v => v.Name)
            .MustAsync(BeUniqueName)
                .WithMessage("'Name' must be unique.");
    }

    public async Task<bool> BeUniqueName(UpdateDepartmentCommand updateDepartment, 
        string name, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AllAsync(x => !x.Name.Equals(name) || x.DepartmentID == updateDepartment.DepartmentID, cancellationToken);
    }
}
