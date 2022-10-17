using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Queries.GetCourseDetails;

namespace WebUI.Client.Pages.Courses;

public partial class CourseDetails
{
    [Inject]
    public IStringLocalizer<CourseDetails> Localizer { get; set; }

    [Inject]
    public ICourseService CourseService { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public int CourseId { get; set; }

    public CourseDetailVM Course { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Course = await CourseService.GetAsync(CourseId.ToString());
    }

    public void Close()
    {
        MudDialog.Cancel();
    }
}
