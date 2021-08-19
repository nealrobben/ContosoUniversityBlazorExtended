using System.Collections.Generic;

namespace WebUI.Shared.Departments.Queries.GetDepartmentsOverview
{
    public class DepartmentsOverviewVM
    {
        public IList<DepartmentVM> Departments { get; set; }

        public DepartmentsOverviewVM()
        {
            Departments = new List<DepartmentVM>();
        }

        public DepartmentsOverviewVM(IList<DepartmentVM> departments)
        {
            Departments = departments;
        }
    }
}
