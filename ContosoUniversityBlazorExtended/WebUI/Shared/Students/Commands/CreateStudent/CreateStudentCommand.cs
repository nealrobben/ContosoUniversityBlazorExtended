using MediatR;
using System;

namespace WebUI.Shared.Students.Commands.CreateStudent
{
    public class CreateStudentCommand : IRequest
    {
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime EnrollmentDate { get; set; }
    }
}
