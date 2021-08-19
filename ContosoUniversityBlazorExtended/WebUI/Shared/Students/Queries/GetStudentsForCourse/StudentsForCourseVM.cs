using System.Collections.Generic;

namespace WebUI.Shared.Students.Queries.GetStudentsForCourse
{
    public class StudentsForCourseVM
    {
        public IList<StudentForCourseVM> Students { get; set; }

        public StudentsForCourseVM()
        {
            Students = new List<StudentForCourseVM>();
        }

        public StudentsForCourseVM(IList<StudentForCourseVM> students)
        {
            Students = students;
        }
    }
}
