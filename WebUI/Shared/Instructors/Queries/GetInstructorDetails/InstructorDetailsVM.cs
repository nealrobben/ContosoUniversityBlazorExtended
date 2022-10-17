using AutoMapper;
using ContosoUniversityBlazor.Domain.Entities;
using System;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Instructors.Queries.GetInstructorDetails;

public class InstructorDetailsVM : IMapFrom<Instructor>
{
    public int InstructorID { get; set; }

    public string LastName { get; set; }

    public string FirstName { get; set; }

    public DateTime HireDate { get; set; }

    public string OfficeLocation { get; set; }

    public string ProfilePictureName { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Instructor, InstructorDetailsVM>()
            .ForMember(d => d.InstructorID, opt => opt.MapFrom(s => s.ID))
            .ForMember(d => d.FirstName, opt => opt.MapFrom(s => s.FirstMidName))
            .ForMember(d => d.OfficeLocation, opt => opt.MapFrom(s => s.OfficeAssignment != null ? s.OfficeAssignment.Location : string.Empty));
    }

    public override string ToString()
    {
        return $"{InstructorID} - {LastName} - {FirstName}";
    }
}
