using System;
using System.ComponentModel.DataAnnotations;

namespace WebUI.Shared.Home.Queries.GetAboutInfo
{
    public class EnrollmentDateGroup
    {
        [DataType(DataType.Date)]
        public DateTime? EnrollmentDate { get; set; }

        public int StudentCount { get; set; }
    }
}
