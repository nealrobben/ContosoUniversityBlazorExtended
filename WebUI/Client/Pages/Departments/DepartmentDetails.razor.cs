using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Services;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;

namespace WebUI.Client.Pages.Departments;

public partial class DepartmentDetails
{
    [CascadingParameter] 
    MudDialogInstance MudDialog { get; set; }

    [Inject]
    private IDepartmentService DepartmentService { get; set; }

    [Inject]
    private IStringLocalizer<DepartmentDetails> Localizer { get; set; }

    public DepartmentDetailVM Department { get; set; }

    [Parameter] 
    public int DepartmentId { get; set; }

    protected async override Task OnParametersSetAsync()
    {
        Department = await DepartmentService.GetAsync(DepartmentId.ToString());
    }

    public void Close()
    {
        MudDialog.Cancel();
    }
}
