using ContosoUniversityBlazor.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.CreateDepartment;

namespace Application.Departments.Validators;

public class CreateDepartmentValidator 
    : WebUI.Shared.Departments.Validators.CreateDepartmentValidator
{
    private readonly ISchoolContext _context;

    public CreateDepartmentValidator(ISchoolContext context) : base()
    {
        _context = context;

        RuleFor(v => v.Name)
            .MustAsync(BeUniqueName)
                .WithMessage("'Name' must be unique.");
    }

    public async Task<bool> BeUniqueName(CreateDepartmentCommand createDepartment, 
        string name, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .AllAsync(x => !x.Name.Equals(name), cancellationToken);
    }
}
