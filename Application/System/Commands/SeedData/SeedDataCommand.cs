using ContosoUniversityBlazor.Application.Common.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ContosoUniversityBlazor.Application.System.Commands.SeedData;

public class SeedDataCommand : IRequest
{
}

public class SeedDataCommandHandler : IRequestHandler<SeedDataCommand>
{
    private readonly ISchoolContext _schoolContext;

    public SeedDataCommandHandler(ISchoolContext schoolContext)
    {
        _schoolContext = schoolContext;
    }

    public async Task<Unit> Handle(SeedDataCommand request, CancellationToken cancellationToken)
    {
        var seeder = new DataSeeder(_schoolContext);
        await seeder.Seed();

        return Unit.Value;
    }
}
