namespace ContosoUniversityBlazor.Domain.Entities;

public class Person
{
    public int ID { get; set; }
    public string LastName { get; set; }
    public string FirstMidName { get; set; }
    public string ProfilePictureName { get; set; }

    public string FullName
    {
        get
        {
            return LastName + ", " + FirstMidName;
        }
    }

    public override string ToString()
    {
        return FullName;
    }
}
