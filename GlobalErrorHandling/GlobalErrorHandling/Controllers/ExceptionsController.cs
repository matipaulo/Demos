namespace GlobalErrorHandling.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("exceptions")]
    public class ExceptionsController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Problem();
        }
    }
}
