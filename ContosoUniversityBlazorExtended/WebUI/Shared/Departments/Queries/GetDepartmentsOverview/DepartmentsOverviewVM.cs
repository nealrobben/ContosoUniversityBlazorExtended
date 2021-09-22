﻿using System.Collections.Generic;
using WebUI.Shared.Common;

namespace WebUI.Shared.Departments.Queries.GetDepartmentsOverview
{
    public class DepartmentsOverviewVM
    {
        public IList<DepartmentVM> Departments { get; set; }

        public MetaData MetaData { get; set; }

        public DepartmentsOverviewVM()
        {
            Departments = new List<DepartmentVM>();
            MetaData = new MetaData();
            MetaData.PageNumber = 0;
        }

        public DepartmentsOverviewVM(IList<DepartmentVM> departments)
        {
            if (departments != null)
                Departments = departments;
            else
                Departments = new List<DepartmentVM>();

            MetaData = new MetaData();
            MetaData.PageNumber = 0;
        }
    }
}
