using AutoMapper;
using ContosoUniversityBlazor.Domain.Entities;
using ContosoUniversityBlazor.Domain.Enums;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Students.Queries.GetStudentsForCourse;

public class StudentForCourseVM : IMapFrom<Enrollment>
{
    public string StudentName { get; set; }
    public Grade? StudentGrade { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Enrollment, StudentForCourseVM>()
            .ForMember(d => d.StudentName, opt => opt.MapFrom(s => s.Student.FullName))
            .ForMember(d => d.StudentGrade, opt => opt.MapFrom(s => s.Grade));
    }

    public override string ToString()
    {
        return $"{StudentName} - {StudentGrade}";
    }
}
