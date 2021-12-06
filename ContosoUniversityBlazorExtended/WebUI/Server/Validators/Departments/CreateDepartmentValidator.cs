using ContosoUniversityBlazor.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.CreateDepartment;

namespace WebUI.Server.Validators.Departments
{
    public class CreateDepartmentValidator : Shared.Departments.Validators.CreateDepartmentValidator
    {
        private readonly SchoolContext _context;

        public CreateDepartmentValidator(SchoolContext context) : base()
        {
            _context = context;

            RuleFor(v => v.Name)
                .MustAsync(BeUniqueName)
                    .WithMessage("'Name' must be unique.");
        }

        public async Task<bool> BeUniqueName(CreateDepartmentCommand createDepartment, string name, CancellationToken cancellationToken)
        {
            return await _context.Departments
                .AllAsync(x => x.Name != name, cancellationToken);
        }
    }
}
