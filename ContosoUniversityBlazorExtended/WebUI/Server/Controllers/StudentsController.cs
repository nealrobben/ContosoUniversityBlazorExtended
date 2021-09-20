using ContosoUniversityBlazor.Application.Students.Commands.DeleteStudent;
using ContosoUniversityBlazor.Application.Students.Queries.GetStudentDetails;
using ContosoUniversityBlazor.Application.Students.Queries.GetStudentsForCourse;
using ContosoUniversityBlazor.Application.Students.Queries.GetStudentsOverview;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WebUI.Shared.Students.Commands.CreateStudent;
using WebUI.Shared.Students.Commands.UpdateStudent;
using WebUI.Shared.Students.Queries.GetStudentDetails;
using WebUI.Shared.Students.Queries.GetStudentsForCourse;
using WebUI.Shared.Students.Queries.GetStudentsOverview;

namespace ContosoUniversityBlazor.WebUI.Controllers
{
    public class StudentsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<StudentsOverviewVM>> GetAll(string sortOrder, string currentFilter, string searchString, int? pageNumber, int? pageSize)
        {
            var vm = await Mediator.Send(new GetStudentsOverviewQuery(sortOrder, currentFilter, searchString, pageNumber, pageSize));

            return Ok(vm);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<StudentDetailsVM>> Get(string id)
        {
            var vm = await Mediator.Send(new GetStudentDetailsQuery(int.Parse(id)));

            return Ok(vm);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(string id)
        {
            await Mediator.Send(new DeleteStudentCommand(int.Parse(id)));

            return NoContent();
        }

        [HttpGet("bycourse/{id}")]
        public async Task<ActionResult<StudentsForCourseVM>> ByCourse(string id)
        {
            var vm = await Mediator.Send(new GetStudentsForCourseQuery(int.Parse(id)));

            return Ok(vm);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateStudentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Update([FromBody] UpdateStudentCommand command)
        {
            await Mediator.Send(command);

            return NoContent();
        }
    }
}
