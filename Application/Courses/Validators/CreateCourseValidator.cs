using ContosoUniversityBlazor.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Commands.CreateCourse;

namespace Application.Courses.Validators;

public class CreateCourseValidator
    : WebUI.Shared.Courses.Validators.CreateCourseValidator
{
    private readonly ISchoolContext _context;

    public CreateCourseValidator(ISchoolContext context) : base()
    {
        _context = context;

        RuleFor(v => v.Title)
            .MustAsync(BeUniqueTitle)
                .WithMessage("'Title' must be unique.");
    }

    public async Task<bool> BeUniqueTitle(CreateCourseCommand createCourse,
        string title, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .AllAsync(x => !x.Title.Equals(title), cancellationToken);
    }
}
