using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Services;
using WebUI.Shared.Common;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;

namespace WebUI.Client.Pages.Instructors;

public partial class Instructors
{
    [Inject]
    public IStringLocalizer<Instructors> Localizer { get; set; }

    [Inject]
    public IInstructorService InstructorService { get; set; }

    [Inject]
    public ISnackbar SnackBar { get; set; }

    [Inject]
    public IDialogService _dialogService { get; set; }

    private MudTable<InstructorVM> Table;

    public OverviewVM<InstructorVM> InstructorsOverview { get; set; } = new OverviewVM<InstructorVM>();

    public int? SelectedInstructorId { get; set; }
    public int? SelectedCourseId { get; set; }

    protected override void OnInitialized()
    {
        SnackBar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
        SnackBar.Configuration.ClearAfterNavigation = true;
    }

    private async Task GetInstructors()
    {
        await Table.ReloadServerData();
    }

    public async Task DeleteInstructor(int instructorId, string name)
    {
        bool? dialogResult = await _dialogService.ShowMessageBox(Localizer["Confirm"], Localizer["DeleteConfirmation", name],
            yesText: Localizer["Delete"], cancelText: Localizer["Cancel"]);

        if (dialogResult == true)
        {
            try
            {
                await InstructorService.DeleteAsync(instructorId.ToString());

                SnackBar.Add(Localizer["DeleteFeedback", name], Severity.Success);
                await GetInstructors();
            }
            catch (System.Exception)
            {
                SnackBar.Add(Localizer["DeleteErrorFeedback", name], Severity.Error);
            }
        }
    }

    public void SelectInstructor(int instructorId)
    {
        SelectedInstructorId = instructorId;
        SelectedCourseId = null;
    }

    public void OnCourseSelected(int courseId)
    {
        SelectedCourseId = courseId;
    }

    public void OpenInstructorDetails(int instructorId)
    {
        var parameters = new DialogParameters();
        parameters.Add("InstructorId", instructorId);

        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        _dialogService.Show<InstructorDetails>(Localizer["InstructorDetails"], parameters, options);
    }

    public async Task OpenInstructorEdit(int instructorId)
    {
        var parameters = new DialogParameters();
        parameters.Add("InstructorId", instructorId);

        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = _dialogService.Show<InstructorEdit>(Localizer["InstructorEdit"], parameters, options);

        var result = await dialog.Result;

        if (result.Data != null && (bool)result.Data)
        {
            await GetInstructors();
        }
    }

    public async Task OpenCreateInstructor()
    {
        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

        var dialog = _dialogService.Show<InstructorCreate>(Localizer["CreateInstructor"], options);
        var result = await dialog.Result;

        if (result.Data != null && (bool)result.Data)
        {
            await GetInstructors();
        }
    }

    public async Task Filter()
    {
        await GetInstructors();
    }

    public async Task BackToFullList()
    {
        InstructorsOverview.MetaData.SearchString = "";
        await GetInstructors();
    }

    public async Task<TableData<InstructorVM>> ServerReload(TableState state)
    {
        var searchString = InstructorsOverview?.MetaData.SearchString ?? "";
        var sortString = state.GetSortString();

        var result = await InstructorService.GetAllAsync(sortString, state.Page, searchString, state.PageSize);

        return new TableData<InstructorVM>() { TotalItems = result.MetaData.TotalRecords, Items = result.Records };
    }

    public string InstructorsSelectRowClassFunc(InstructorVM instructor, int rowNumber)
    {
        if (instructor?.InstructorID == SelectedInstructorId)
            return "mud-theme-primary";

        return "";
    }
}
