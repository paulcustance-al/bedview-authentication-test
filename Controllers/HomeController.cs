using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace test_wa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }   

        public IActionResult Index()
        {
            ViewData["authenticated"] = User.Identity.IsAuthenticated;
            ViewData["name"] = User.Identity.Name;

            return View();
        }
    }
}
