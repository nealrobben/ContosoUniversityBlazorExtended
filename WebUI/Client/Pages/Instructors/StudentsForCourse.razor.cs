using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace WebUI.Client.Pages.Instructors;

public partial class StudentsForCourse
{
    [Inject]
    public IStringLocalizer<Instructors> Localizer { get; set; }

    [Inject]
    public IStudentService StudentService { get; set; }

    [Parameter]
    public int? SelectedCourseId { get; set; }

    public StudentsForCourseVM StudentsForCourseVM { get; set; } = new StudentsForCourseVM();

    protected override async Task OnParametersSetAsync()
    {
        if(SelectedCourseId != null)
            StudentsForCourseVM = await StudentService.GetStudentsForCourse(SelectedCourseId.ToString());
    }
}
