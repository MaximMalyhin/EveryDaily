using Microsoft.AspNetCore.Mvc;

namespace EveryDaily.Api.Controllers
{
    public class ReportController : ControllerBase
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
