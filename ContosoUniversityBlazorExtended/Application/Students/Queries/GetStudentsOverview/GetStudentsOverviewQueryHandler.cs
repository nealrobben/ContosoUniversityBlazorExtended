using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace ContosoUniversityBlazor.Application.Students.Queries.GetStudentsOverview
{
    public class GetStudentsOverviewQueryHandler : IRequestHandler<GetStudentsOverviewQuery, StudentsOverviewVM>
    {
        private const int _pageSize = 3;

        private readonly ISchoolContext _context;
        private readonly IMapper _mapper;

        public GetStudentsOverviewQueryHandler(ISchoolContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<StudentsOverviewVM> Handle(GetStudentsOverviewQuery request, CancellationToken cancellationToken)
        {
            var result = new StudentsOverviewVM();

            result.CurrentSort = request.SortOrder;
            result.NameSortParm = string.IsNullOrEmpty(request.SortOrder) ? "name_desc" : "";
            result.DateSortParm = request.SortOrder == "Date" ? "date_desc" : "Date";

            if (request.SearchString != null)
            {
                request.PageNumber = 1;
            }
            else
            {
                result.CurrentFilter = request.SearchString;
            }

            var students = from s in _context.Students
                           select s;
            if (!string.IsNullOrEmpty(request.SearchString))
            {
                students = students.Where(s => s.LastName.Contains(request.SearchString)
                                       || s.FirstMidName.Contains(request.SearchString));
            }

            switch (result.CurrentSort)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.LastName);
                    break;
            }

            var numberOfPages = (await students.CountAsync() / (double)_pageSize);
            result.TotalPages = (int)Math.Ceiling(numberOfPages);
            result.PageNumber = request.PageNumber ?? 1;

            var items = await students.AsNoTracking().Skip((result.PageNumber - 1) * _pageSize)
                .Take(_pageSize)
                .ProjectTo<StudentOverviewVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            result.AddStudents(items);

            return result;
        }
    }
}
