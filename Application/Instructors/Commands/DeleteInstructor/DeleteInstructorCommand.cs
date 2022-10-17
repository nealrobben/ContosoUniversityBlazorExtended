using MediatR;

namespace ContosoUniversityBlazor.Application.Instructors.Commands.DeleteInstructor;

public class DeleteInstructorCommand : IRequest
{
    public int ID { get; set; }

    public DeleteInstructorCommand(int id)
    {
        ID = id;
    }
}
