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
using WebUI.Shared.Common;

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

            var students = _context.Students
                .Search(request.SearchString)
                .Sort(request.SortOrder);

            var totalStudents = await students.CountAsync();

            result.MetaData = new MetaData(request.PageNumber ?? 0, totalStudents, 
                request.PageSize ?? _defaultPageSize, request.SortOrder, request.SearchString);

            var items = await students.AsNoTracking().Skip((result.MetaData.PageNumber) * result.MetaData.PageSize)
                .Take(result.MetaData.PageSize)
                .ProjectTo<StudentOverviewVM>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            result.AddStudents(items);

            return result;
        }
    }
}
