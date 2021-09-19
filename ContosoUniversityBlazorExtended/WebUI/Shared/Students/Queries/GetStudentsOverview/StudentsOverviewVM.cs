using System.Collections.Generic;

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
            MetaData.PageNumber = 1;
        }

        public StudentsOverviewVM(IList<StudentOverviewVM> students)
        {
            if (students != null)
                Students = students;
            else
                Students = new List<StudentOverviewVM>();

            MetaData = new MetaData();
            MetaData.PageNumber = 1;
        }

        public void AddStudents(List<StudentOverviewVM> students)
        {
            foreach (var student in students)
            {
                Students.Add(student);
            }
        }
    }

    public class MetaData
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public string CurrentSort { get; set; }

        public string NameSortParm { get; set; }

        public string DateSortParm { get; set; }

        public string CurrentFilter { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
