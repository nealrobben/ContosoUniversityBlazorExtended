using ContosoUniversityBlazor.Application.Departments.Commands.DeleteDepartment;
using ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentDetails;
using ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsOverview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ContosoUniversityBlazor.Application.Departments.Queries.GetDepartmentsLookup;
using WebUI.Shared.Departments.Queries.GetDepartmentsOverview;
using WebUI.Shared.Departments.Queries.GetDepartmentDetails;
using WebUI.Shared.Departments.Commands.CreateDepartment;
using WebUI.Shared.Departments.Commands.UpdateDepartment;
using WebUI.Shared.Departments.Queries.GetDepartmentsLookup;
using WebUI.Shared.Common;

namespace ContosoUniversityBlazor.WebUI.Controllers;

public class DepartmentsController : ContosoApiController
{
    [HttpGet]
    public async Task<ActionResult<OverviewVM<DepartmentVM>>> GetAll(string sortOrder, string searchString, int? pageNumber, int? pageSize)
    {
        var vm = await Mediator.Send(new GetDepartmentsOverviewQuery(sortOrder, searchString, pageNumber, pageSize));

        return Ok(vm);
    }

    [HttpGet("{id}", Name = "GetDepartment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<DepartmentDetailVM>> Get(string id)
    {
        var vm = await Mediator.Send(new GetDepartmentDetailsQuery(int.Parse(id)));

        return Ok(vm);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
    {
        var departmentID = await Mediator.Send(command);
        var result = await Mediator.Send(new GetDepartmentDetailsQuery(departmentID));

        return CreatedAtRoute("GetDepartment", new { id = departmentID.ToString() }, result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteDepartmentCommand(int.Parse(id)));

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpGet("lookup")]
    public async Task<ActionResult<DepartmentsLookupVM>> GetLookup()
    {
        var vm = await Mediator.Send(new GetDepartmentsLookupQuery());

        return Ok(vm);
    }
}
