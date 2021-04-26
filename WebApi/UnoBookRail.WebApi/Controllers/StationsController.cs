using Microsoft.AspNetCore.Mvc;
using UnoBookRail.Common.Network;

namespace UnoBookRail.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StationsController : ControllerBase
    {
        public StationsController()
        {
        }

        [HttpGet]
        public Arrivals GetNextArrivals(int stationId)
        {
            return new Stations().GetNextArrivals(stationId);
        }
    }
}
