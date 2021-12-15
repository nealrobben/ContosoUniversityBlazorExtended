using MediatR;
using System;

namespace WebUI.Shared.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest<int>
    {
        public string Name { get; set; }

        public decimal Budget { get; set; }

        public DateTime StartDate { get; set; }

        public int InstructorID { get; set; }
    }
}
