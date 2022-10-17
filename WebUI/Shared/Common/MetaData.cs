using System;

namespace WebUI.Shared.Common;

public class MetaData
{
    public int PageNumber { get; set; }

    public int TotalPages { get; set; }

    public int PageSize { get; set; }

    public int TotalRecords { get; set; }

    public string CurrentSort { get; set; }

    public string SearchString { get; set; }

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
