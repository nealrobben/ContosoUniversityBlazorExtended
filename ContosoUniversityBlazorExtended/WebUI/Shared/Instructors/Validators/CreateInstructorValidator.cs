using FluentValidation;
using WebUI.Shared.Instructors.Commands.CreateInstructor;

namespace WebUI.Shared.Instructors.Validators
{
    public class CreateInstructorValidator : AbstractValidator<CreateInstructorCommand>
    {
        public CreateInstructorValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty();
            RuleFor(p => p.LastName).NotEmpty();
            RuleFor(p => p.HireDate).NotEmpty();
        }
    }
}
