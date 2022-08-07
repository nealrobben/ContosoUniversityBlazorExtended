using System.Collections.Generic;

namespace WebUI.Shared.Instructors.Queries.GetInstructorsLookup
{
    public class InstructorsLookupVM
    {
        public IList<InstructorLookupVM> Instructors { get; set; }

        public InstructorsLookupVM()
        {
            Instructors = new List<InstructorLookupVM>();
        }

        public InstructorsLookupVM(IList<InstructorLookupVM> instructors)
        {
            Instructors = instructors;
        }
    }
}
