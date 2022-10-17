using ContosoUniversityBlazor.Application.Common.Interfaces;
using ContosoUniversityBlazor.Domain.Entities;
using ContosoUniversityBlazor.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversityBlazor.Application.System.Commands.SeedData;

public class DataSeeder
{
    private readonly ISchoolContext _context;

    public DataSeeder(ISchoolContext context)
    {
        _context = context;
    }

    public async Task Seed()
    {
        // Look for any students.
        if (_context.Students.Any())
        {
            return;   // DB has been seeded
        }

        Student[] students = await AddStudents();

        Instructor[] instructors = await AddInstructors();

        Department[] departments = await AddDepartments(instructors);

        Course[] courses = await AddCourses(departments);

        await AddOfficeAssignments(instructors);

        await AddCourseAssignments(instructors, courses);

        await AddEnrollments(students, courses);
    }

    private async Task AddEnrollments(Student[] students, Course[] courses)
    {
        var enrollments = new Enrollment[]
        {
            new Enrollment {
                StudentID = students.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                Grade = Grade.A
            },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                Grade = Grade.C
                },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Alexander").ID,
                CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                Grade = Grade.B
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                Grade = Grade.B
                },
                new Enrollment {
                    StudentID = students.Single(s => s.LastName == "Alonso").ID,
                CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                Grade = Grade.B
                },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Alonso").ID,
                CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                Grade = Grade.B
                },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Anand").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
                },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Anand").ID,
                CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
                Grade = Grade.B
                },
            new Enrollment {
                StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
                CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
                Grade = Grade.B
                },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Li").ID,
                CourseID = courses.Single(c => c.Title == "Composition").CourseID,
                Grade = Grade.B
                },
                new Enrollment {
                StudentID = students.Single(s => s.LastName == "Justice").ID,
                CourseID = courses.Single(c => c.Title == "Literature").CourseID,
                Grade = Grade.B
                }
        };

        foreach (Enrollment e in enrollments)
        {
            var enrollmentInDataBase = _context.Enrollments.Where(
                s =>
                        s.Student.ID == e.StudentID &&
                        s.Course.CourseID == e.CourseID).SingleOrDefault();
            if (enrollmentInDataBase == null)
            {
                _context.Enrollments.Add(e);
            }
        }
        await _context.SaveChangesAsync();
    }

    private async Task AddCourseAssignments(Instructor[] instructors, Course[] courses)
    {
        var courseInstructors = new Domain.Entities.CourseAssignment[]
        {
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Harui").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                },
            new Domain.Entities.CourseAssignment {
                CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
                InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
                },
        };

        foreach (Domain.Entities.CourseAssignment ci in courseInstructors)
        {
            _context.CourseAssignments.Add(ci);
        }
        await _context.SaveChangesAsync();
    }

    private async Task AddOfficeAssignments(Instructor[] instructors)
    {
        var officeAssignments = new OfficeAssignment[]
        {
            new OfficeAssignment {
                InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID,
                Location = "Smith 17" },
            new OfficeAssignment {
                InstructorID = instructors.Single( i => i.LastName == "Harui").ID,
                Location = "Gowan 27" },
            new OfficeAssignment {
                InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID,
                Location = "Thompson 304" },
        };

        foreach (OfficeAssignment o in officeAssignments)
        {
            _context.OfficeAssignments.Add(o);
        }
        await _context.SaveChangesAsync();
    }

    private async Task<Course[]> AddCourses(Department[] departments)
    {
        var courses = new Course[]
        {
            new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
            },
            new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
            },
            new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
            },
            new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
                DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
            },
            new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
                DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
            },
            new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
                DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
            },
            new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
                DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
            },
        };

        foreach (Course c in courses)
        {
            _context.Courses.Add(c);
        }
        await _context.SaveChangesAsync();
        return courses;
    }

    private async Task<Department[]> AddDepartments(Instructor[] instructors)
    {
        var departments = new Department[]
        {
            new Department { Name = "English",     Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
            new Department { Name = "Mathematics", Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
            new Department { Name = "Engineering", Budget = 350000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
            new Department { Name = "Economics",   Budget = 100000,
                StartDate = DateTime.Parse("2007-09-01"),
                InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
        };

        foreach (Department d in departments)
        {
            _context.Departments.Add(d);
        }
        await _context.SaveChangesAsync();
        return departments;
    }

    private async Task<Instructor[]> AddInstructors()
    {
        var instructors = new Instructor[]
        {
            new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
                HireDate = DateTime.Parse("1995-03-11") },
            new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
                HireDate = DateTime.Parse("2002-07-06") },
            new Instructor { FirstMidName = "Roger",   LastName = "Harui",
                HireDate = DateTime.Parse("1998-07-01") },
            new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
                HireDate = DateTime.Parse("2001-01-15") },
            new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
                HireDate = DateTime.Parse("2004-02-12") }
        };

        foreach (Instructor i in instructors)
        {
            _context.Instructors.Add(i);
        }
        await _context.SaveChangesAsync();
        return instructors;
    }

    private async Task<Student[]> AddStudents()
    {
        var students = new Student[]
                    {
            new Student { FirstMidName = "Carson",   LastName = "Alexander",
                EnrollmentDate = DateTime.Parse("2010-09-01") },
            new Student { FirstMidName = "Meredith", LastName = "Alonso",
                EnrollmentDate = DateTime.Parse("2012-09-01") },
            new Student { FirstMidName = "Arturo",   LastName = "Anand",
                EnrollmentDate = DateTime.Parse("2013-09-01") },
            new Student { FirstMidName = "Gytis",    LastName = "Barzdukas",
                EnrollmentDate = DateTime.Parse("2012-09-01") },
            new Student { FirstMidName = "Yan",      LastName = "Li",
                EnrollmentDate = DateTime.Parse("2012-09-01") },
            new Student { FirstMidName = "Peggy",    LastName = "Justice",
                EnrollmentDate = DateTime.Parse("2011-09-01") },
            new Student { FirstMidName = "Laura",    LastName = "Norman",
                EnrollmentDate = DateTime.Parse("2013-09-01") },
            new Student { FirstMidName = "Nino",     LastName = "Olivetto",
                EnrollmentDate = DateTime.Parse("2005-09-01") }
                    };

        foreach (Student s in students)
        {
            _context.Students.Add(s);
        }
        await _context.SaveChangesAsync();
        return students;
    }
}
