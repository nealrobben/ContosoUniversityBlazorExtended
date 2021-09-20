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
using Application.Common.Extensions;

namespace ContosoUniversityBlazor.Application.Students.Queries.GetStudentsOverview
{
    public class GetStudentsOverviewQueryHandler : IRequestHandler<GetStudentsOverviewQuery, StudentsOverviewVM>
    {
        private const int _defaultPageSize = 3;

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

            result.MetaData.CurrentSort = request.SortOrder;
            result.MetaData.CurrentFilter = request.SearchString;

            var students = _context.Students
                .Search(request.SearchString)
                .Sort(result.MetaData.CurrentSort);

            var pageSize = request.PageSize ?? _defaultPageSize;

            var totalStudents = await students.CountAsync();
            var numberOfPages = (totalStudents / (double)pageSize);
            result.MetaData.TotalRecords = totalStudents;
            result.MetaData.TotalPages = (int)Math.Ceiling(numberOfPages);
            result.MetaData.PageNumber = request.PageNumber ?? 0;

            var items = await students.AsNoTracking().Skip((result.MetaData.PageNumber) * pageSize)
                .Take(pageSize)
                .ProjectTo<StudentOverviewVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            result.AddStudents(items);

            return result;
        }
    }
}
