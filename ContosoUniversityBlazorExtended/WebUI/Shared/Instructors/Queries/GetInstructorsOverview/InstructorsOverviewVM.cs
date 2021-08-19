using System.Collections.Generic;

namespace WebUI.Shared.Instructors.Queries.GetInstructorsOverview
{
    public class InstructorsOverviewVM
    {
        public IList<InstructorVM> Instructors { get; set; }

        public InstructorsOverviewVM()
        {
            Instructors = new List<InstructorVM>();
        }
        
        public InstructorsOverviewVM(IList<InstructorVM> instructors)
        {
            Instructors = instructors;
        }
    }
}