using FluentValidation;
using WebUI.Shared.Students.Commands.CreateStudent;

namespace WebUI.Shared.Students.Validators
{
    public class CreateStudentValidator : AbstractValidator<CreateStudentCommand>
    {
        public CreateStudentValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.EnrollmentDate).NotEmpty();
        }
    }
}
