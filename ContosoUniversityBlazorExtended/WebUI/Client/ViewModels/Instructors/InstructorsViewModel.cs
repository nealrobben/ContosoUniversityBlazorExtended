using Microsoft.Extensions.Localization;
using MudBlazor;
using System.Threading.Tasks;
using WebUI.Client.Extensions;
using WebUI.Client.Pages.Instructors;
using WebUI.Client.Services;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Instructors.Queries.GetInstructorsOverview;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;

namespace WebUI.Client.ViewModels.Instructors
{
    public class InstructorsViewModel : InstructorViewModelBase
    {
        private readonly ICourseService _courseService;
        private readonly StudentService _studentService;
        private ISnackbar _snackbar { get; set; }
        private IDialogService _dialogService { get; set; }

        public MudTable<InstructorVM> Table { get; set; }

        public InstructorsOverviewVM InstructorsOverview { get; set; } = new InstructorsOverviewVM();
        public CoursesForInstructorOverviewVM CourseForInstructorOverview { get; set; }
        public StudentsForCourseVM StudentsForCourse { get; set; }

        public int? SelectedInstructorId { get; set; }
        public int? SelectedCourseId { get; set; }

        public InstructorsViewModel(IInstructorService instructorService, 
            ICourseService courseService, StudentService studentService, 
            IDialogService dialogService, ISnackbar snackbar,
            IStringLocalizer<InstructorResources> instructorLocalizer,
            IStringLocalizer<GeneralResources> generalLocalizer)
            : base(instructorService,instructorLocalizer,generalLocalizer)
        {
            _courseService = courseService;
            _studentService = studentService;
            _dialogService = dialogService;
            _snackbar = snackbar;
            _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomRight;
            _snackbar.Configuration.ClearAfterNavigation = true;
        }

        private async Task GetInstructors()
        {
            await Table.ReloadServerData();
        }

        public async Task DeleteInstructor(int instructorId, string name)
        {
            bool? dialogResult = await _dialogService.ShowMessageBox(_generalLocalizer["Confirm"], _instructorLocalizer["DeleteConfirmation", name],
                yesText: _generalLocalizer["Delete"], cancelText: _generalLocalizer["Cancel"]);
            
            if (dialogResult == true)
            {
                var result = await _instructorService.DeleteAsync(instructorId.ToString());

                if (result.IsSuccessStatusCode)
                {
                    _snackbar.Add(_instructorLocalizer["DeleteFeedback", name], Severity.Success);
                    await GetInstructors();
                }
            }
        }

        public async Task SelectInstructor(int instructorId)
        {
            SelectedInstructorId = instructorId;
            SelectedCourseId = null;
            StudentsForCourse = null;

            CourseForInstructorOverview = await _courseService.GetCoursesForInstructor(instructorId.ToString());
        }

        public async Task SelectCourse(int courseId)
        {
            SelectedCourseId = courseId;

            StudentsForCourse = await _studentService.GetStudentsForCourse(courseId.ToString());
        }

        public void OpenInstructorDetails(int instructorId)
        {
            var parameters = new DialogParameters();
            parameters.Add("InstructorId", instructorId);

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

            _dialogService.Show<InstructorDetails>(_instructorLocalizer["InstructorDetails"], parameters, options);
        }

        public async Task OpenInstructorEdit(int instructorId)
        {
            var parameters = new DialogParameters();
            parameters.Add("InstructorId", instructorId.ToString());

            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

            var dialog = _dialogService.Show<InstructorEdit>(_instructorLocalizer["InstructorEdit"], parameters, options);

            var result = await dialog.Result;

            if (result.Data != null && (bool)result.Data)
            {
                await GetInstructors();
            }
        }

        public async Task OpenCreateInstructor()
        {
            DialogOptions options = new DialogOptions() { MaxWidth = MaxWidth.Medium };

            var dialog = _dialogService.Show<InstructorCreate>(_instructorLocalizer["CreateInstructor"], options);
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

            var result = await _instructorService.GetAllAsync(sortString, state.Page, searchString, state.PageSize);

            return new TableData<InstructorVM>() { TotalItems = result.MetaData.TotalRecords, Items = result.Instructors };
        }

        public string InstructorsSelectRowClassFunc(InstructorVM instructor, int rowNumber)
        {
            if (instructor?.InstructorID == SelectedInstructorId)
                return "mud-theme-primary";

            return "";
        }

        public string CoursesSelectRowClassFunc(CourseForInstructorVM course, int rowNumber)
        {
            if (course?.CourseID == SelectedCourseId)
                return "mud-theme-primary";

            return "";
        }
    }
}
