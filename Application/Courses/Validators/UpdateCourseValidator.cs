using ContosoUniversityBlazor.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Commands.UpdateCourse;

namespace Application.Courses.Validators;

public class UpdateCourseValidator
    : WebUI.Shared.Courses.Validators.UpdateCourseValidator
{
    private readonly ISchoolContext _context;

    public UpdateCourseValidator(ISchoolContext context) : base()
    {
        _context = context;

        RuleFor(v => v.Title)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'Title' must be unique.");
    }

    public async Task<bool> BeUniqueTitle(UpdateCourseCommand updateCourse,
        string title, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .AllAsync(x => !x.Title.Equals(title) || x.CourseID == updateCourse.CourseID, cancellationToken);
    }
}
