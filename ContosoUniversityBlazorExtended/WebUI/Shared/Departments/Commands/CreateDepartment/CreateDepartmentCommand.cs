using MediatR;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Shared.Departments.Commands.CreateDepartment
{
    public class CreateDepartmentCommand : IRequest
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Budget { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public int InstructorID { get; set; }
    }
}
