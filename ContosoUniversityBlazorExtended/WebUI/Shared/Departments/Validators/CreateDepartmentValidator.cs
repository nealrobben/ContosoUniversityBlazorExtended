using FluentValidation;
using WebUI.Shared.Departments.Commands.CreateDepartment;

namespace WebUI.Shared.Departments.Validators
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Budget).NotEmpty();
            RuleFor(p => p.StartDate).NotEmpty();
            RuleFor(p => p.InstructorID).NotEmpty();
        }
    }
}
