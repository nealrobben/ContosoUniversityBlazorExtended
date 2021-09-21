using System;

namespace WebUI.Shared.Students.Queries.GetStudentsOverview
{
    public class MetaData
    {
        public int PageNumber { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

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

        public MetaData()
        {
        }

        public MetaData(int pageNumber, int totalRecords, int pageSize, string currentSort, string searchString)
        {
            PageNumber = pageNumber;
            TotalRecords = totalRecords;
            PageSize = pageSize;
            CurrentSort = currentSort;
            SearchString = searchString;

            var numberOfPages = (TotalRecords / (double)PageSize);
            TotalPages = (int)Math.Ceiling(numberOfPages);
        }
    }
}
