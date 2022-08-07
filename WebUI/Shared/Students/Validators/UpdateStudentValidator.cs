using FluentValidation;
using WebUI.Shared.Students.Commands.UpdateStudent;

namespace WebUI.Shared.Students.Validators
{
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.LastName).NotEmpty().MaximumLength(50);
            RuleFor(p => p.EnrollmentDate).NotEmpty();
        }
    }
}
