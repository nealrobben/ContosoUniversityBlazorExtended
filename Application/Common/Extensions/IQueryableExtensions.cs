using ContosoUniversityBlazor.Domain.Entities;
using System;
using System.Linq;

namespace Application.Common.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<Student> Search(this IQueryable<Student> value, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return value;

        return value.Where(s => s.LastName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                   || s.FirstMidName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
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

        return value.Where(s => s.Name.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
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

    public static IQueryable<Instructor> Search(this IQueryable<Instructor> value, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return value;

        return value.Where(s => s.LastName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase)
                   || s.FirstMidName.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IQueryable<Instructor> Sort(this IQueryable<Instructor> value, string sortString)
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
            case "hiredate_asc":
                return value.OrderBy(s => s.HireDate);
            case "hiredate_desc":
                return value.OrderByDescending(s => s.HireDate);
            default:
                return value.OrderBy(s => s.LastName);
        }
    }

    public static IQueryable<Course> Search(this IQueryable<Course> value, string searchString)
    {
        if (string.IsNullOrWhiteSpace(searchString))
            return value;

        return value.Where(s => s.Title.Contains(searchString, StringComparison.CurrentCultureIgnoreCase));
    }

    public static IQueryable<Course> Sort(this IQueryable<Course> value, string sortString)
    {
        switch (sortString?.ToLower() ?? "")
        {
            case "title_asc":
                return value.OrderBy(s => s.Title);
            case "title_desc":
                return value.OrderByDescending(s => s.Title);
            case "courseid_asc":
                return value.OrderBy(s => s.CourseID);
            case "courseid_desc":
                return value.OrderByDescending(s => s.CourseID);
            case "credits_asc":
                return value.OrderBy(s => s.Credits);
            case "credits_desc":
                return value.OrderByDescending(s => s.Credits);
            default:
                return value.OrderBy(s => s.Title);
        }
    }
}
