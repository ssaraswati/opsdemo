using Microsoft.AspNetCore.Mvc;

namespace ApiLog.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthzController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get() => "OK";
    }
}