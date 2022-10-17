using System;
using System.Collections.Generic;

namespace ContosoUniversityBlazor.Domain.Entities;

public class Student : Person
{
    public Student()
    {
        Enrollments = new HashSet<Enrollment>();
    }

    public DateTime EnrollmentDate { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}
