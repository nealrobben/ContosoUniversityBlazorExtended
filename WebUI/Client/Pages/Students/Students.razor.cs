using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Services;
using WebUI.Shared.Common;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace WebUI.Client.Pages.Students;

public partial class Students
{
    [Inject]
    public IStringLocalizer<Students> Localizer { get; set; }

    [Inject]
    public IDialogService _dialogService { get; set; }

    [Inject]
    public IStudentService StudentService { get; set; }

    [Inject]
    public ISnackbar _snackbar { get; set; }

    private MudTable<StudentOverviewVM> Table;

    public OverviewVM<StudentOverviewVM> StudentsOverview { get; set; } = new OverviewVM<StudentOverviewVM>();

    protected override void OnInitialized()
    {
        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
        _snackbar.Configuration.ClearAfterNavigation = true;
    }

    private async Task GetStudents()
    {
        await Table.ReloadServerData();
    }

    public async Task DeleteStudent(int studentId, string name)
    {
        bool? dialogResult = await _dialogService.ShowMessageBox(Localizer["Confirm"], Localizer["DeleteConfirmation", name],
            yesText: Localizer["Delete"], cancelText: Localizer["Cancel"]);

        if (dialogResult == true)
        {
            try
            {
                await StudentService.DeleteAsync(studentId.ToString());

                _snackbar.Add(Localizer["DeleteFeedback", name], Severity.Success);
                await GetStudents();
            }
            catch (System.Exception)
            {
                _snackbar.Add(Localizer["DeleteErrorFeedback", name], Severity.Error);
            }
        }
    }

    public async Task Filter()
    {
        await GetStudents();
    }

    public async Task BackToFullList()
    {
        StudentsOverview.MetaData.SearchString = "";
        await GetStudents();
    }

    public void OpenStudentDetails(int studentId)
    {
        var parameters = new DialogParameters();
        parameters.Add("StudentId", studentId);

        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        _dialogService.Show<StudentDetails>(Localizer["StudentDetails"], parameters, options);
    }

    public async Task OpenStudentEdit(int studentId)
    {
        var parameters = new DialogParameters();
        parameters.Add("StudentId", studentId);

        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = _dialogService.Show<StudentEdit>(Localizer["StudentEdit"], parameters, options);

        var result = await dialog.Result;

        if (result.Data != null && (bool)result.Data)
        {
            await GetStudents();
        }
    }

    public async Task OpenStudentCreate()
    {
        DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Large };

        var dialog = _dialogService.Show<StudentCreate>(Localizer["CreateStudent"], options);
        var result = await dialog.Result;

        if (result.Data != null && (bool)result.Data)
        {
            await GetStudents();
        }
    }

    public async Task<TableData<StudentOverviewVM>> ServerReload(TableState state)
    {
        var searchString = StudentsOverview?.MetaData.SearchString ?? "";
        var sortString = state.GetSortString();

        var result = await StudentService.GetAllAsync(sortString, state.Page, searchString, state.PageSize);

        return new TableData<StudentOverviewVM>() { TotalItems = result.MetaData.TotalRecords, Items = result.Records };
    }
}
