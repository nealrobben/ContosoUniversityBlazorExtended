using System.Collections.Generic;
using WebUI.Shared.Common;

namespace WebUI.Shared.Courses.Queries.GetCoursesOverview
{
    public class CoursesOverviewVM
    {
        public IList<CourseVM> Courses { get; set; }

        public MetaData MetaData { get; set; }

        public CoursesOverviewVM()
        {
            Courses = new List<CourseVM>();
            MetaData = new MetaData();
        }

        public CoursesOverviewVM(IList<CourseVM> courses, MetaData metaData)
        {
            if (courses != null)
                Courses = courses;
            else
                Courses = new List<CourseVM>();

            MetaData = metaData;
        }
    }
}
