namespace WebUI.Shared.Students.Queries.GetStudentsOverview
{
    public class MetaData
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int TotalRecords { get; set; }

        public string CurrentSort { get; set; }

        public string SearchString { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageNumber > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageNumber < TotalPages);
            }
        }
    }
}
