using AutoMapper;
using ContosoUniversityBlazor.Domain.Entities;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Courses.Queries.GetCoursesOverview;

public class CourseVM : IMapFrom<Course>
{
    public int CourseID { get; set; }

    public string Title { get; set; }

    public int Credits { get; set; }

    public string DepartmentName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Course, CourseVM>()
            .ForMember(d => d.DepartmentName, opt => opt.MapFrom(s => s.Department.Name));
    }

    public override string ToString()
    {
        return $"{CourseID} - {Title} - {DepartmentName}";
    }
}
