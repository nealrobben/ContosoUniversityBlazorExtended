using ContosoUniversityBlazor.Application.Home.Queries.GetAboutInfo;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;
using WebUI.Shared.Home.Queries.GetAboutInfo;

namespace ContosoUniversityBlazor.WebUI.Controllers;

public class AboutController : ContosoApiController
{
    [HttpGet]
    public async Task<ActionResult<AboutInfoVM>> GetAboutInfo()
    {
        //To demo the loading indicator
        Thread.Sleep(2000);

        var result = await Mediator.Send(new GetAboutInfoQuery());

        return Ok(result);
    }
}
