using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Students.Queries.GetStudentDetails;

namespace WebUI.Client.Pages.Students;

public partial class StudentDetails
{
    [Inject]
    public IStudentService StudentService { get; set; }

    [Inject]
    IStringLocalizer<StudentDetails> Localizer { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public int StudentId { get; set; }

    public StudentDetailsVM Student { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Student = await StudentService.GetAsync(StudentId.ToString());
    }

    public void Close()
    {
        MudDialog.Cancel();
    }
}
