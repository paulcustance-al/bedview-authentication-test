using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using System.Threading.Tasks;

namespace test_wa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }   

        [Authorize]
        public IActionResult Index()
        {
            ViewData["authenticated"] = User.Identity.IsAuthenticated;
            ViewData["name"] = User.Identity.Name;

            // The user used as Log On as for the Windows Service
            var serviceUser = WindowsIdentity.GetCurrent().Name;

            ViewData["serviceUser"] = serviceUser;
    
            // The user to be impersonated
            var userToImpersonate = (WindowsIdentity)HttpContext.User.Identity;

            ViewData["userToImpersonate"] = userToImpersonate;

            // Impersonating the current Windows user [HttpContext.User.Identity]...
            WindowsIdentity.RunImpersonated(userToImpersonate.AccessToken, () =>
            {
                // This time WindowsIdentity.GetCurrent() will retrieve the impersonated
                // user with its claims...
                var impersonatedUser = WindowsIdentity.GetCurrent().Name;

                ViewData["impersonatedUser"] = impersonatedUser;

                // Your business logic code here...  
            });

            return View();
        }
    }
}
