using System.Collections.Generic;

namespace WebUI.Shared.Home.Queries.GetAboutInfo
{
    public class AboutInfoVM
    {
        public List<EnrollmentDateGroup> Items { get; set; }

        public AboutInfoVM()
        {
            Items = new List<EnrollmentDateGroup>();
        }

        public AboutInfoVM(List<EnrollmentDateGroup> items)
        {
            Items = items;
        }
    }
}
