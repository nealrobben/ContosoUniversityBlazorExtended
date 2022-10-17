using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Instructors.Queries.GetInstructorDetails;

namespace WebUI.Client.Pages.Instructors;

public partial class InstructorDetails
{
    [Inject]
    public IStringLocalizer<InstructorDetails> Localizer { get; set; }

    [Inject]
    public IInstructorService InstructorService { get; set; }

    [CascadingParameter]
    MudDialogInstance MudDialog { get; set; }

    [Parameter]
    public int InstructorId { get; set; }

    public InstructorDetailsVM Instructor { get; set; }

    protected override async Task OnParametersSetAsync()
    {
        Instructor = await InstructorService.GetAsync(InstructorId.ToString());
    }

    public void Close()
    {
        MudDialog.Cancel();
    }
}
