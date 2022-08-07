using FluentValidation;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Shared.Students.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.EnrollmentDate).NotEmpty();
        }
    }
}
