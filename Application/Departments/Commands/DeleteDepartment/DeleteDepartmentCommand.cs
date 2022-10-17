using MediatR;

namespace ContosoUniversityBlazor.Application.Departments.Commands.DeleteDepartment;

public class DeleteDepartmentCommand : IRequest
{
    public int ID { get; set; }

    public DeleteDepartmentCommand(int id)
    {
        ID = id;
    }
}
