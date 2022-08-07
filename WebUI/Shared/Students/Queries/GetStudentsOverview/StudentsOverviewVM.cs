using System.Collections.Generic;
using WebUI.Shared.Common;

namespace WebUI.Shared.Students.Queries.GetStudentsOverview
{
    public class StudentsOverviewVM
    {
        public IList<StudentOverviewVM> Students { get; set; }

        public MetaData MetaData { get; set; }

        public StudentsOverviewVM()
        {
            Students = new List<StudentOverviewVM>();
            MetaData = new MetaData();
            MetaData.PageNumber = 0;
        }

        public StudentsOverviewVM(IList<StudentOverviewVM> students, MetaData metaData)
        {
            if (students != null)
                Students = students;
            else
                Students = new List<StudentOverviewVM>();

            MetaData = metaData;
        }
    }
}
