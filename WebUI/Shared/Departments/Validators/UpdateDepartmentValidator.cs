using FluentValidation;
using WebUI.Shared.Departments.Commands.UpdateDepartment;

namespace WebUI.Shared.Departments.Validators;

public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
{
    public UpdateDepartmentValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Budget).NotEmpty().GreaterThan(0);
        RuleFor(p => p.StartDate).NotEmpty();
        RuleFor(p => p.InstructorID).NotEmpty();
    }
}
