using ContosoUniversityBlazor.Application.Courses.Commands.DeleteCourse;
using ContosoUniversityBlazor.Application.Courses.Queries.GetCourseDetails;
using ContosoUniversityBlazor.Application.Courses.Queries.GetCoursesOverview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Shared.Courses.Commands.CreateCourse;
using WebUI.Shared.Courses.Commands.UpdateCourse;
using WebUI.Shared.Courses.Queries.GetCourseDetails;
using WebUI.Shared.Courses.Queries.GetCoursesForInstructor;
using WebUI.Shared.Courses.Queries.GetCoursesOverview;

namespace ContosoUniversityBlazor.WebUI.Controllers
{
    public class CoursesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<CoursesOverviewVM>> GetAll()
        {
            var vm = await Mediator.Send(new GetCoursesOverviewQuery());

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CourseDetailVM>> Get(string id)
        {
            var vm = await Mediator.Send(new GetCourseDetailsQuery(int.Parse(id)));

            return Ok(vm);
        }

        [HttpGet("byinstructor/{id}")]
        public async Task<ActionResult<CoursesForInstructorOverviewVM>> ByInstructor(string id)
        {
            var vm = await Mediator.Send(new GetCoursesForInstructorQuery(int.Parse(id)));

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteCourseCommand(int.Parse(id)));

            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateCourseCommand command)
        {
            await Mediator.Send(command);

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
    }
}
