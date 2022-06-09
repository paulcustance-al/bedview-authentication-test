using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BedViewMock.Models;
using AuthenticationService.Managers;
using Microsoft.Extensions.Configuration;
using AuthenticationService.Models;
using System.Security.Claims;
namespace BedViewMock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;
        private readonly IAuthContainerModel _authContainerModel;

        public HomeController(
            ILogger<HomeController> logger,
            IAuthService authService,
            IConfiguration configuration,
            IAuthContainerModel authContainerModel)
        {
            _logger = logger;
            _authService = authService;
            _configuration = configuration;
            _authContainerModel = authContainerModel;
        }

        public IActionResult Index()
        {
            _authContainerModel.AddClaim(new Claim(ClaimTypes.NameIdentifier, "PHT_MASTER\\stevenk1"));
            var token = _authService.GenerateToken(_authContainerModel);

            ViewData["token"] = token;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
