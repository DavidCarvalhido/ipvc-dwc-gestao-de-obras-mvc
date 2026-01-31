using Microsoft.AspNetCore.Mvc;

namespace MVCTrabDWC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
