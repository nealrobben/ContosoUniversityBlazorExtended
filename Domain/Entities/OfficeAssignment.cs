namespace ContosoUniversityBlazor.Domain.Entities;

public class OfficeAssignment
{
    public int InstructorID { get; set; }
    public string Location { get; set; }

    public Instructor Instructor { get; set; }
}
