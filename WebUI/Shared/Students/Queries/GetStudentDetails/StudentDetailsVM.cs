using AutoMapper;
using ContosoUniversityBlazor.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Students.Queries.GetStudentDetails
{
    public class StudentDetailsVM : IMapFrom<Student>
    {
        public int StudentID { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public string ProfilePictureName { get; set; }

        public List<StudentDetailsEnrollmentVM> Enrollments { get; set; }

        public StudentDetailsVM()
        {
            Enrollments = new List<StudentDetailsEnrollmentVM>();
        }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Student, StudentDetailsVM>()
                .ForMember(d => d.StudentID, opt => opt.MapFrom(s => s.ID))
                .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstMidName));
        }
    }
}