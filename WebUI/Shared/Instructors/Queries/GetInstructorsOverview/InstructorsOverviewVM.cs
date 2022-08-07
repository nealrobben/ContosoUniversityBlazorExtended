using System.Collections.Generic;
using WebUI.Shared.Common;

namespace WebUI.Shared.Instructors.Queries.GetInstructorsOverview
{
    public class InstructorsOverviewVM
    {
        public IList<InstructorVM> Instructors { get; set; }

        public MetaData MetaData { get; set; }

        public InstructorsOverviewVM()
        {
            Instructors = new List<InstructorVM>();
            MetaData = new MetaData();
        }
        
        public InstructorsOverviewVM(IList<InstructorVM> instructors, MetaData metaData)
        {
            if (instructors != null)
                Instructors = instructors;
            else
                Instructors = new List<InstructorVM>();

            MetaData = metaData;
        }
    }
}