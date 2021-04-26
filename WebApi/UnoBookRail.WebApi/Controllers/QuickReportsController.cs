using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace UnoBookRail.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuickReportsController : ControllerBase
    {
        public QuickReportsController()
        {
        }

        [HttpPost("Create")]
        public async Task<ActionResult<string>> CreateReport([FromForm] IFormFile imageFile, [FromForm] string location, [FromForm] string information)
        {
            // A real app would do something with the provided data
            System.Diagnostics.Debug.WriteLine($"{imageFile} {location} {information}");

            return await Task.FromResult<ActionResult<string>>("success");
        }
    }
}
