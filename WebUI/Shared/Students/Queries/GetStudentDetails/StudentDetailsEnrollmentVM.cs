using AutoMapper;
using ContosoUniversityBlazor.Domain.Entities;
using ContosoUniversityBlazor.Domain.Enums;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Students.Queries.GetStudentDetails;

public class StudentDetailsEnrollmentVM : IMapFrom<Enrollment>
{
    public string CourseTitle { get; set; }

    public Grade? Grade { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Enrollment, StudentDetailsEnrollmentVM>()
            .ForMember(d => d.CourseTitle, opt => opt.MapFrom(s => s.Course.Title))
            .ForMember(d => d.Grade, opt => opt.MapFrom(s => s.Grade));
    }
}