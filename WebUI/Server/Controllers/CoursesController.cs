using ContosoUniversityBlazor.Application.Courses.Commands.DeleteCourse;
using ContosoUniversityBlazor.Application.Courses.Queries.GetCourseDetails;
using ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Shared.Common;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace ContosoUniversityBlazor.WebUI.Controllers;

public class CoursesController : ContosoApiController
{
    [HttpGet]
    public async Task<ActionResult<OverviewVM<CourseVM>>> GetAll(string sortOrder, string searchString, int? pageNumber, int? pageSize)
    {
        var vm = await Mediator.Send(new GetCoursesOverviewQuery(sortOrder, searchString, pageNumber, pageSize));

        return Ok(vm);
    }

    [HttpGet("{id}", Name = "GetCourse")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<CourseDetailVM>> Get(string id)
    {
        var vm = await Mediator.Send(new GetCourseDetailsQuery(int.Parse(id)));

        return Ok(vm);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
    {
        var courseID = await Mediator.Send(command);

        var result = await Mediator.Send(new GetCourseDetailsQuery(courseID));

        return CreatedAtRoute("GetCourse", new { id = courseID.ToString() }, result);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        await Mediator.Send(new DeleteCourseCommand(int.Parse(id)));

        return NoContent();
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> Update([FromBody] UpdateCourseCommand command)
    {
        await Mediator.Send(command);

        return NoContent();
    }

    [HttpGet("byinstructor/{id}")]
    public async Task<ActionResult<CoursesForInstructorOverviewVM>> ByInstructor(string id)
    {
        var vm = await Mediator.Send(new GetCoursesForInstructorQuery(int.Parse(id)));

        return Ok(vm);
    }
}
