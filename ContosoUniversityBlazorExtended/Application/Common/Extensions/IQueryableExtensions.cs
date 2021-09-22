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
            switch (sortString?.ToLower() ?? "")
            {
                case "lastname_asc":
                    return value.OrderBy(s => s.LastName);
                case "lastname_desc":
                    return value.OrderByDescending(s => s.LastName);
                case "firstname_asc":
                    return value.OrderBy(s => s.FirstMidName);
                case "firstname_desc":
                    return value.OrderByDescending(s => s.FirstMidName);
                case "enrollmentdate_asc":
                    return value.OrderBy(s => s.EnrollmentDate);
                case "enrollmentdate_desc":
                    return value.OrderByDescending(s => s.EnrollmentDate);
                default:
                    return value.OrderBy(s => s.LastName);
            }
        }

        public static IQueryable<Department> Search(this IQueryable<Department> value, string searchString)
        {
            if (string.IsNullOrWhiteSpace(searchString))
                return value;

            return value.Where(s => s.Name.Contains(searchString));
        }

        public static IQueryable<Department> Sort(this IQueryable<Department> value, string sortString)
        {
            switch (sortString?.ToLower() ?? "")
            {
                case "name_asc":
                    return value.OrderBy(s => s.Name);
                case "name_desc":
                    return value.OrderByDescending(s => s.Name);
                default:
                    return value.OrderBy(s => s.Name);
            }
        }
    }
}
