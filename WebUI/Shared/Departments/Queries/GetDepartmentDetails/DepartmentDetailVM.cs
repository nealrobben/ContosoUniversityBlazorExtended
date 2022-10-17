using AutoMapper;
using ContosoUniversityBlazor.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using WebUI.Shared.Common.Mappings;

namespace WebUI.Shared.Departments.Queries.GetDepartmentDetails;

public class DepartmentDetailVM : IMapFrom<Department>
{
    public int DepartmentID { get; set; }

    public string Name { get; set; }

    [DataType(DataType.Currency)]
    public decimal Budget { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime StartDate { get; set; }

    public string AdministratorName { get; set; }

    public int? InstructorID { get; set; }

    public byte[] RowVersion { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Department, DepartmentDetailVM>()
            .ForMember(d => d.AdministratorName, opt => opt.MapFrom(s => s.Administrator != null ? s.Administrator.FullName : string.Empty));
    }

    public override string ToString()
    {
        return $"{DepartmentID} - {Name}";
    }
}
