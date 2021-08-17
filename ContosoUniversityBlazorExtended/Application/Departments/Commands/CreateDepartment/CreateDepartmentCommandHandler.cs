using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Departments.Commands.CreateDepartment;

namespace ContosoUniversityBlazor.Application.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand>
    {
        private readonly ISchoolContext _context;

        public CreateDepartmentCommandHandler(ISchoolContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            _context.Departments.Add(new Department
            {
                Name = request.Name,
                Budget = request.Budget,
                StartDate = request.StartDate,
                InstructorID = request.InstructorID
            });

            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
