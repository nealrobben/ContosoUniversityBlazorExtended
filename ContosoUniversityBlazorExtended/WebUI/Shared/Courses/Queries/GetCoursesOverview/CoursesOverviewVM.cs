using System.Collections.Generic;

namespace WebUI.Shared.Courses.Queries.GetCoursesOverview
{
    public class CoursesOverviewVM
    {
        public IList<CourseVM> Courses { get; set; }

        public CoursesOverviewVM()
        {
            Courses = new List<CourseVM>();
        }

        public CoursesOverviewVM(IList<CourseVM> courses)
        {
            Courses = courses;
        }
    }
}
