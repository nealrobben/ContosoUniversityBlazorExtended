using FluentValidation;
using WebUI.Shared.Courses.Commands.UpdateCourse;

namespace WebUI.Shared.Courses.Validators;

public class UpdateCourseValidator : AbstractValidator<UpdateCourseCommand>
{
    public UpdateCourseValidator()
    {
        RuleFor(p => p.CourseID).NotEmpty();
        RuleFor(p => p.Title).NotEmpty().MaximumLength(50);
        RuleFor(p => p.Credits).NotEmpty().GreaterThan(0);
        RuleFor(p => p.DepartmentID).NotEmpty();
    }
}
