using System.Collections.Generic;

namespace ContosoUniversityBlazor.Domain.Entities;

public class Course
{
    public Course()
    {
        CourseAssignments = new HashSet<CourseAssignment>();
        Enrollments = new HashSet<Enrollment>();
    }

    public int CourseID { get; set; }
    public string Title { get; set; }
    public int Credits { get; set; }
    public int DepartmentID { get; set; }

    public Department Department { get; set; }
    public ICollection<Enrollment> Enrollments { get; set; }
    public ICollection<CourseAssignment> CourseAssignments { get; set; }

    public override string ToString()
    {
        return Title;
    }
}
