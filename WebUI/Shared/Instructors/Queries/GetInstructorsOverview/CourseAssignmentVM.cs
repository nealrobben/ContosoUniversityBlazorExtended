using AutoMapper;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Instructors.Queries.GetInstructorsOverview
{
    public class CourseAssignmentVM : IMapFrom<ContosoUniversityBlazor.Domain.Entities.CourseAssignment>
    {
        public int CourseID { get; set; }

        public string CourseTitle { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<ContosoUniversityBlazor.Domain.Entities.CourseAssignment, CourseAssignmentVM>()
                .ForMember(d => d.CourseTitle, opt => opt.MapFrom(s => s.Course.Title));
        }
    }
}
