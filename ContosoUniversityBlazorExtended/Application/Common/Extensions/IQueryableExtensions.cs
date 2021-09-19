using ContosoUniversityBlazor.Domain.Entities;
using System.Linq;

namespace Application.Common.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<Student> Search(this IQueryable<Student> value, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return value;

            return value.Where(s => s.LastName.Contains(searchString)
                       || s.FirstMidName.Contains(searchString));
        }

        public static IQueryable<Student> Sort(this IQueryable<Student> value, string sortString)
        {
            switch (sortString)
            {
                case "name_desc":
                    return value.OrderByDescending(s => s.LastName);
                case "Date":
                    return value.OrderBy(s => s.EnrollmentDate);
                case "date_desc":
                    return value.OrderByDescending(s => s.EnrollmentDate);
                default:
                    return value.OrderBy(s => s.LastName);
            }
        }
    }
}
