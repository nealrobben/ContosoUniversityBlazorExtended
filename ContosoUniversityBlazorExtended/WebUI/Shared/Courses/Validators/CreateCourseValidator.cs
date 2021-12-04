using FluentValidation;
using WebUI.Shared.Courses.Commands.CreateCourse;

namespace WebUI.Shared.Courses.Validators
{
    public class CreateCourseValidator : AbstractValidator<CreateCourseCommand>
    {
        public CreateCourseValidator()
        {
            RuleFor(p => p.CourseID).NotEmpty();
            RuleFor(p => p.Title).NotEmpty().MaximumLength(50);
            RuleFor(p => p.Credits).NotEmpty().GreaterThan(0);
        }
    }
}
